using AutoMapper;
using LibraryAPI.Dtos;
using LibraryAPI.Models;
using LibraryAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

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

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> Get( string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return new EmptyResult();
            }
            var books = await _bookRepository.GetManyAsync(search);
            var bookDtoList = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(bookDtoList);
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
