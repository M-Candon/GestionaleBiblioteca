using GestionaleBiblioteca.Server.Data;
using GestionaleBiblioteca.Server.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Enable CORS for development
// 1. In builder.Services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", // Diamo un nome alla policy
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Più sicuro di AllowAnyOrigin
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true; // optional, easyer JSON readability
}); // Register controllers
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlite("Data Source=library.db")); // Use SQLite database


var app = builder.Build();

// Development-specific middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting(); // Routing middleware
app.UseCors("AllowAngular"); // Enable CORS
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Map controller endpoints

#if DEBUG
// Automatically create database and seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    db.Database.EnsureCreated(); // create DB if it doesn't exist

    // Seed data only if database is empty
    if (!db.Authors.Any() || !db.Books.Any())
    {
        // Create authors
        var autore1 = new Author { Name = "George", Surname = "Orwell" };
        var autore2 = new Author { Name = "J.K.", Surname = "Rowling" };
        var autore3 = new Author { Name = "J.R.R.", Surname = "Tolkien" };

        db.Authors.AddRange(autore1, autore2, autore3);
        db.SaveChanges();

        // Create books
        var libro1 = new Book { Title = "1984", Genre="Fantascienza", PublishingYear = 1949, AuthorId = autore1.Id };
        var libro2 = new Book { Title = "Harry Potter e la Pietra Filosofale", Genre = "Fantasy", PublishingYear = 1997, AuthorId = autore2.Id };
        var libro3 = new Book { Title = "Il Signore degli Anelli", Genre= "Fantasy", PublishingYear = 1954, AuthorId = autore3.Id };

        db.Books.AddRange(libro1, libro2, libro3);
        db.SaveChanges();
    }
}
#else

// Automatically create the database if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    db.Database.EnsureCreated();
}

#endif
app.Run(); // Start the application
