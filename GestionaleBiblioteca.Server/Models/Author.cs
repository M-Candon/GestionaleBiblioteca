using System.Text.Json.Serialization;

namespace GestionaleBiblioteca.Server.Models
{
    public class Author
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the author.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Surname of the author.
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        // Navigation property: one author can have many books
        /// <summary>
        /// List of books written by the author.
        /// </summary>
        [JsonIgnore] // prevent cycles when serializing
        public List<Book> Libri { get; set; } = new();
    }
}
