using GestionaleBiblioteca.Server.Data;
using GestionaleBiblioteca.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/authors")]
public class AuthorsController : ControllerBase
{
    #region Fields

    private readonly LibraryContext _context;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of <see cref="AuthorsController"/>.
    /// </summary>
    /// <param name="context"></param>
    public AuthorsController(LibraryContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Retrieves all authors.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var authors = await _context.Authors.ToListAsync();
        return Ok(authors);
    }

    /// <summary>
    /// Creates a new author.
    /// </summary>
    /// <param name="author">Author instance to be created.</param>
    [HttpPost]
    public async Task<IActionResult> CreateAuthor(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return Ok(author);
    }

    /// <summary>
    /// Deletes an author by ID.
    /// </summary>
    /// <param name="id">ID of the author to be deleted.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) return NotFound();

        // Note: If the author has linked books, SQLite will throw error Foreign Key.
        // TODO manage this case properly.
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// Update an existing author.
    /// </summary>
    /// <param name="id">ID of the author instance.</param>
    /// <param name="author">Author instance.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, Author author)
    {
        if (id != author.Id) return BadRequest();

        _context.Entry(author).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Authors.Any(e => e.Id == id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    #endregion
}