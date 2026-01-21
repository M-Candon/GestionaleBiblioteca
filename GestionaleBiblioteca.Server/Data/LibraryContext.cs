using Microsoft.EntityFrameworkCore;

namespace GestionaleBiblioteca.Server.Data
{
    public class LibraryContext : DbContext
    {
        #region Constructors
        
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Represents the collection of books in the library.
        /// </summary>
        public DbSet<Models.Book> Books { get; set; } = null!; // null-forgiving operator

        /// <summary>
        /// Represents the collection of authors in the library.
        /// </summary>
        public DbSet<Models.Author> Authors { get; set; } = null!; // null-forgiving operator

        #endregion
    }
}
