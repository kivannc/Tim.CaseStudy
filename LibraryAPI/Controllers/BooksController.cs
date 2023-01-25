using AutoMapper;
using LibraryAPI.Dtos;
using LibraryAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<IEnumerable<BookDto>>(await _bookRepository.GetAllBooksAsync()));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return new EmptyResult();
            }
            var books = await _bookRepository.GetManyAsync(search);
            var bookDtoList = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(bookDtoList);
        }
    }
}
