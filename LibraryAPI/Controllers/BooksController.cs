using AutoMapper;
using LibraryAPI.Dtos;
using LibraryAPI.Models;
using LibraryAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private const int MaxAllowedReturnDay = 30;


        private readonly IBookRepository _bookRepository;
        private readonly IHolidayRepository _holidayRepository;
        private readonly IMapper _mapper;
 

        public BooksController(IBookRepository bookRepository, IMapper mapper, IHolidayRepository holidayRepository, ITransactionRepository transactionRepository, IMemberRepository memberRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _holidayRepository = holidayRepository;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<BookDto>>> Search([FromBody] BookSearchDto bookSearch)
        {
            if (bookSearch == null || 
                (string.IsNullOrWhiteSpace(bookSearch.ISBN) &&
                 string.IsNullOrWhiteSpace(bookSearch.Name) &&
                 string.IsNullOrWhiteSpace(bookSearch.Author)))
            {
                return BadRequest();
            }
            var book = _mapper.Map<Book>(bookSearch);
            var books = await _bookRepository.GetManyAsync(book);
            var bookDtoList = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(bookDtoList.OrderBy(b=> b.BookStatus));
        }

        [HttpGet("{isbn}" , Name = "GetBookDetail")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBookDetail(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                return BadRequest();
            }

            var book = await _bookRepository.GetBookByIdAsync(isbn);
            var bookDto = _mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }

        [HttpGet("GetDueDate")]
        public async Task<ActionResult<DueDateDto>> GetDueDate()
        {
            var holidays = await _holidayRepository.GetHolidays();
            var dateOnlyHolidays = holidays.Select(h => DateOnly.FromDateTime(h.Date)).Distinct().ToArray();
            var workingDays = 0;
            var checkedDay = DateTime.Now;
            while (workingDays < MaxAllowedReturnDay)
            {
                if (checkedDay.DayOfWeek == DayOfWeek.Saturday || checkedDay.DayOfWeek == DayOfWeek.Sunday)
                {
                    checkedDay = checkedDay.AddDays(1);
                    continue;
                }

                if (dateOnlyHolidays.Contains(DateOnly.FromDateTime(checkedDay)))
                {
                    checkedDay = checkedDay.AddDays(1);
                    continue;
                }
                checkedDay  = checkedDay.AddDays(1);
                workingDays++;
            }

            return Ok(new DueDateDto { DueDate = checkedDay});
        }


    }
}
