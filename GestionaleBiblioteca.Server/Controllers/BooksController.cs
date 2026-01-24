using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionaleBiblioteca.Server.Data;
using GestionaleBiblioteca.Server.Models;

namespace GestionaleBiblioteca.Server.Controllers;

[ApiController]
[Route("api/books")] // Base route for the controller
public class BooksController : ControllerBase
{
    private readonly LibraryContext _context;

    #region Constructors

    /// <summary>
    /// Creates a new instance of the booksController.
    /// </summary>
    /// <param name="context">Instance of <see cref="LibraryContext"/>.</param>
    public BooksController(LibraryContext context)
    {
        _context = context;
    }

    #endregion

    // GET /api/books
    /// <summary>
    /// Returns all books in the library.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _context.Books
                                        .Include(l => l.Author) // Include related author data
                                        .ToListAsync(); // Fetch all books from database
        return Ok(books); // Return as JSON
    }

    // POST /api/books
    /// <summary>
    /// Adds a new book to the library.
    /// </summary>
    /// <param name="book"></param>
    [HttpPost]
    public async Task<IActionResult> Add(Book book)
    {
        _context.Books.Add(book); // Add book to DbSet
        await _context.SaveChangesAsync(); // Save changes to database
        // Return 201 Created with location header
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    // GET /api/books/{id}
    /// <summary>
    /// Get a single book by ID
    /// </summary>
    /// <param name="id">integer representing the ID of the book.</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var libro = await _context.Books.FindAsync(id); // Search by primary key
        if (libro == null) return NotFound(); // Return 404 if not found
        return Ok(libro); // Return book as JSON
    }

    /// <summary>
    /// Deletes a book by ID.
    /// </summary>
    /// <param name="id">ID of the book to be removed.</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent(); // Returns 204 (success)
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, Book book)
    {
        if (id != book.Id) return BadRequest();

        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Books.Any(e => e.Id == id)) return NotFound();
            else throw;
        }

        return NoContent();
    }
}