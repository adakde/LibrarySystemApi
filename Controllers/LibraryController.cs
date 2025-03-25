using LibaryApi.Data;
using LibaryApi.Entities;
using LibaryApi.Entities.Enum;
using LibaryApi.Models;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LibaryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController : Controller
    {
        private readonly AppDbContext _context;
        public LibraryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooks([FromQuery] string title, [FromQuery] string author)
        {
            var books = await _context.Books
                .Where(b => b.Title.Contains(title) && b.Author.Contains(author))
                .ToListAsync();
            return books;
        }
        [HttpGet("/books/{id}/Available")]
        public async Task<ActionResult<BookAvailabilityDto>> IsBookAvailable(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return new BookAvailabilityDto
            {
                IsAvailable = book.Status == Status.Available,
                Status = book.Status.ToString()
            };
        }
        [HttpGet("/books")]
        public async  Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }
        [HttpGet("/books/{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }
        [HttpPost("/books")]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }
        [HttpPut("/books/{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookDto dto)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            bool statusChanged = existingBook.Status != dto.Status;

            existingBook.Title = dto.Title;
            existingBook.Author = dto.Author;
            existingBook.Status = dto.Status;

            try
            {
                if (statusChanged)
                {
                    var historyEntry = new BookStatusHistory
                    {
                        BookId = id,
                        Status = dto.Status, 
                        ChangeDate = DateTime.UtcNow
                    };
                    _context.BookStatusHistories.Add(historyEntry);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }
        [HttpPut("/books/{id}/status")]
        public async Task<IActionResult> UpdateBookStatus([FromRoute] int id, [FromBody] UpdateBookDto dto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            book.Title = dto.Title;
            book.Author = dto.Author;
            book.Status = dto.Status;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("/books/{id}/reserve")]
        public async Task<IActionResult> ReserveBook([FromRoute] int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            if (book.Status != Status.Available)
            {
                return BadRequest("Book is not available");
            }
            book.Status = Status.Reserved;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("/books/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("/books")]
        public async Task<IActionResult> DeleteAllBooks()
        {
            _context.Books.RemoveRange(_context.Books);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
