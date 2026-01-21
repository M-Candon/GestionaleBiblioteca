using GestionaleBiblioteca.Server.Data;
using GestionaleBiblioteca.Server.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true; // optional, easer JSON readability
}); // Register controllers
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlite("Data Source=library.db")); // Use SQLite database

// Enable CORS for development
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Development-specific middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(); // Enable CORS
app.UseRouting(); // Routing middleware

app.MapControllers(); // Map controller endpoints

#if DEBUG
// Automatically create database and seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    db.Database.EnsureCreated(); // create DB if it doesn't exist

    // Seed data only if database is empty
    if (!db.Authors.Any())
    {
        // Create authors
        var autore1 = new Author { Name = "George", Surname = "Orwell" };
        var autore2 = new Author { Name = "J.K.", Surname = "Rowling" };
        var autore3 = new Author { Name = "J.R.R.", Surname = "Tolkien" };

        db.Authors.AddRange(autore1, autore2, autore3);
        db.SaveChanges();

        // Create books
        var libro1 = new Book { Title = "1984", PublishingYear = 1949, AuthorId = autore1.Id };
        var libro2 = new Book { Title = "Harry Potter e la Pietra Filosofale", PublishingYear = 1997, AuthorId = autore2.Id };
        var libro3 = new Book { Title = "Il Signore degli Anelli", PublishingYear = 1954, AuthorId = autore3.Id };

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
