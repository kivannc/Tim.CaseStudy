using AutoMapper;
using LibraryAPI.Dtos;
using LibraryAPI.Models;
using LibraryAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionController(
            IBookRepository bookRepository, 
            IMemberRepository memberRepository, 
            ITransactionRepository transactionRepository, 
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> Get()
        {
            var transactions = await _transactionRepository.GetAll();
            var transactionDtoList = _mapper.Map<IEnumerable<TransactionDto>>(transactions);
            return Ok(transactionDtoList);
        }

        [HttpGet("{id}",Name = "GetTransaction")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(int id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return Ok(transactionDto);
        }


        [HttpPost]
        public async Task<ActionResult<TransactionDto>> AddTransaction(TransactionDto transactionDto)
        {
            //Check if book exist
            var book = await _bookRepository.GetBookByIdAsync(transactionDto.Book.ISBN);
            if (book == null)
            {
                return NotFound(nameof(Book));
            }

            //Check if member exist 
            var member = await _memberRepository.GetMemberByIdAsync(transactionDto.Member.Id);
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
                BorrowDate = DateTime.Now,
                DueDate = transactionDto.DueDate,
            };

            _transactionRepository.AddBookTransaction(transaction);

            var mappedTransaction = _mapper.Map<TransactionDto>(transaction);
            await _transactionRepository.SaveChangesAsync();

            return CreatedAtRoute(nameof(GetTransaction), new { id = mappedTransaction.Id }, mappedTransaction);
        }
    }
}
