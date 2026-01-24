using System.Text.Json.Serialization;

namespace GestionaleBiblioteca.Server.Models
{
    public class Book
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of the book.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Genre of the book.
        /// </summary>
        public string Genre { get; set; } = string.Empty;
        
        /// <summary>
        /// Author ID.
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Author navigation property.
        /// </summary>
        public virtual Author? Author { get; set; } = null!; // Navigation property

        /// <summary>
        /// Year the book was published.
        /// </summary>
        public int PublishingYear { get; set; }
    }
}
