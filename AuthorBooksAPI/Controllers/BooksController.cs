using AuthorBooksAPI.Application.DTO;
using AuthorBooksAPI.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthorBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _booksService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var book = await _booksService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] BookRequestDto bookRequestDto)
        {
            var result = await _booksService.CreateAsync(bookRequestDto);
            if (result)
            {
                return Ok(new { message = "Book created successfully." });
            }
            else
            {
                return BadRequest(new { message = "Failed to create book." });
            }
        }
    }
}
