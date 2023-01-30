using AutoMapper;
using Library.API.Dtos;
using Library.API.Models;
using Library.API.Repository.Interface;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPenaltyService _penaltyService;
        private readonly IMapper _mapper;

        public TransactionsController(
            IBookRepository bookRepository, 
            IMemberRepository memberRepository, 
            ITransactionRepository transactionRepository, 
            IPenaltyService penaltyService,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _transactionRepository = transactionRepository;
            _penaltyService = penaltyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> Get()
        {
            var transactions = await _transactionRepository.GetAll();
            var transactionDtoList = _mapper.Map<IEnumerable<TransactionDto>>(transactions);
            return Ok(transactionDtoList);
        }

        [HttpGet("{id}", Name = "GetTransactionById")]
        public async Task<ActionResult<TransactionDto>> GetTransactionById(int id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return Ok(transactionDto);
        }

        [HttpGet("isbn/{isbn}" , Name = "GetTransactionByISBN")]
        public async Task<ActionResult<TransactionDto>> GetTransactionByISBN(string isbn)
        {
            var transaction = await _transactionRepository.GetTransactionByISBNAsync(isbn);
            if (transaction == null)
            {
                return NotFound();
            }
            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            transactionDto.Penalty = _penaltyService.CalculatePenalty(transactionDto.LateDays);
            return Ok(transactionDto);
        }


        [HttpPost]
        //TODO ReserveDto will be implemented
        public async Task<ActionResult<TransactionDto>> AddTransaction(ReserveDto reserveDto)
        {
            //Check if book exist
            var book = await _bookRepository.GetBookByIdAsync(reserveDto.ISBN);
            if (book == null)
            {
                return NotFound(nameof(Book));
            }

            //Check if member exist 
            var member = await _memberRepository.GetMemberByIdAsync(reserveDto.MemberId);
            if (member == null)
            {
                return NotFound(nameof(Member));
            }

            //Check if book is available.
            // If there is transaction and its return date  is null it means book reserved 
            var pastTransaction = await _transactionRepository.GetManyAsync(t => t.ISBN == book.ISBN && t.ReturnDate == null);
            if (pastTransaction.Any())
            {
                return Conflict(new {message = "Book is already reserved"});
            }

            var transaction = new BookTransaction
            {
                Book = book,
                Member = member,
                BorrowDate = DateTime.Today,
                DueDate = reserveDto.DueDate
            };

            _transactionRepository.AddBookTransaction(transaction);

            var mappedTransaction = _mapper.Map<TransactionDto>(transaction);
            await _transactionRepository.SaveChangesAsync();

            return CreatedAtRoute(nameof(GetTransactionById), new { id = mappedTransaction.Id }, mappedTransaction);
        }
    }
}
