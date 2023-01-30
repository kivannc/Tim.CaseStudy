using AutoMapper;
using Library.API.Dtos;
using Library.API.Models;
using Library.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
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
            var dateOnlyHolidays = holidays.Select(h => DateOnly.FromDateTime(h.Date)).Distinct().ToList();
            var workingDays = 0;
            var checkedDay = DateTime.Today;
            while (workingDays < MaxAllowedReturnDay)
            {
                if (checkedDay.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
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

            // add weekends to holidays array
            for (DateTime date = DateTime.Today; date.Date <= checkedDay.Date; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    dateOnlyHolidays.Add(DateOnly.FromDateTime(date));
                }
            }

            return Ok(new DueDateDto { DueDate = checkedDay , Holidays = dateOnlyHolidays});
        }


    }
}
