using Library.DAL.Contexts;
using Library.DAL.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DAL.UnitTest.Factory;

public class TestApplicationDbContextFactory
{
    public ApplicationDbContext CreateFilledDbContext()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options;

        var context = new ApplicationDbContext(optionsBuilder);

        if (context.Database.EnsureCreated())
        {
            using var viewCommand = context.Database.GetDbConnection().CreateCommand();
            viewCommand.CommandText = @"
CREATE VIEW AllResources AS
SELECT Name
FROM Author;";
            viewCommand.ExecuteNonQuery();
        }

        var authors = new[]
        {
            new Author { Name = "Author1", Surname = "Author1" },
            new Author { Name = "Author2", Surname = "Author2" }
        };
        
        var subject = new[]
        {
            new Subject() { Name = "Subject1" },
            new Subject() { Name = "Subject2" }
        };

        var books = new[]
        {
            new Book()
            {
                Name = "Book1", ISBN = "123", Author = authors[0], Subject = subject[0]
            },
            new Book()
            {
                Name = "Book2", ISBN = "1234", Author = authors[1], Subject = subject[1]
            }
        };
        
        var formularies = new[]
        {
            new ReaderFormulary()
            {
                Name = "Name1", Surname = "Surname1"
            },
            new ReaderFormulary()
            {
                Name = "Book2", Surname = "Surname1"
            }
        };
        
        formularies[0].TakenBooks.Add(books[0]);
        formularies[1].TakenBooks.Add(books[1]);
        
        


        context.AddRange(authors);
        context.AddRange(books);
        context.AddRange(formularies);
        context.SaveChanges();

        return context;
    }
    
    public ApplicationDbContext CreateAuthorFilledDbContext()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options;

        var context = new ApplicationDbContext(optionsBuilder);

        if (context.Database.EnsureCreated())
        {
            using var viewCommand = context.Database.GetDbConnection().CreateCommand();
            viewCommand.CommandText = @"
CREATE VIEW AllResources AS
SELECT Name
FROM Author;";
            viewCommand.ExecuteNonQuery();
        }

        var authors = new[]
        {
            new Author { Name = "Author1", Surname = "Author1" },
            new Author { Name = "Author2", Surname = "Author2" }
        };

        context.AddRange(authors);
        context.SaveChanges();

        return context;
    }

}