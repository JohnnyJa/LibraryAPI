using DAL.UnitTest.Factory;
using Library.DAL.Entities;
using Library.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace DAL.UnitTest;

public class RepositoryTest
{
    private readonly TestApplicationDbContextFactory _factory = new();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetAuthor_ExistName_AuthorSuccess()
    {
        var unit = new UnitOfWork(_factory.CreateFilledDbContext());

        var author = unit.Author.GetValueOrDefault(a => a.Name == "Author1");
        Assert.That(author!.Name, Is.EqualTo("Author1"));
    }

    [Test]
    public void GetAuthor_NotExistName_AuthorIsNull()
    {
        var unit = new UnitOfWork(_factory.CreateFilledDbContext());

        var author = unit.Author.GetValueOrDefault(a => a.Name == "Author3");
        Assert.That(author, Is.Null);
    }

    [Test]
    public void GetAllAuthor_EmptyCondition_AuthorEnum()
    {
        var unit = new UnitOfWork(_factory.CreateFilledDbContext());


        var author = unit.Author.GetAll();
        Assert.That(author.Count(), Is.EqualTo(2));
    }

    [Test]
    public void AddAuthor_Author_Success()
    {
        var unit = new UnitOfWork(_factory.CreateFilledDbContext());


        var author = new Author()
        {
            Name = "Author4",
            Surname = "Author4"
        };

        unit.Author.Add(author);
        unit.Save();

        var authors = unit.Author.GetAll();
        Assert.That(authors.Count(), Is.EqualTo(3));
    }
    
    [Test]
    public void DeleteAuthor_AuthorExist_Success()
    {
        var unit = new UnitOfWork(_factory.CreateAuthorFilledDbContext());
        
        var author = unit.Author.GetValueOrDefault(a => a.Surname == "Author1");

        unit.Author.Delete(author!);
        unit.Save();

        var authors = unit.Author.GetAll();
        Assert.That(authors.Count(), Is.EqualTo(1));
    }
    
    [Test]
    public void DeleteRangeAuthor_AuthorList_Success()
    {
        var unit = new UnitOfWork(_factory.CreateAuthorFilledDbContext());


        var authorsBefore = unit.Author.GetAll();

        unit.Author.DeleteRange(authorsBefore);
        unit.Save();    

        var authorsAfter = unit.Author.GetAll();
        Assert.That(authorsAfter.Count(), Is.EqualTo(0));
    }
    
    [Test]
    public void DeleteAuthor_AuthorNotExist_Error()
    {
        var unit = new UnitOfWork(_factory.CreateFilledDbContext());


        var author = new Author
        {
            Name = "1",
            Surname = "q"
        };

        Assert.Throws<DbUpdateConcurrencyException>(() =>
        {
            unit.Author.Delete(author);
            unit.Save();

        });
        
        var authors = unit.Author.GetAll();
        Assert.That(authors.Count(), Is.EqualTo(2));
    }
    
    [Test]
    public void UpdateAuthor_AuthorExist_Success()
    {
        var unit = new UnitOfWork(_factory.CreateFilledDbContext());


        var author = unit.Author.GetValueOrDefault(a => a.Surname == "Author1");

        author!.Name = "Author3";
        author.Surname = "author3";
        
        unit.Author.Update(author);
        unit.Save();

        author = unit.Author.GetValueOrDefault(a => a.Name == "Author3");
        Assert.That(author!.Surname, Is.EqualTo("author3"));
    }

    [Test]
    public void GetFormularyWithBooksAndAuthors_FormularyExistWithIncludeParam_FormularyWithBooksAndAuthors()
    {
        var unit = new UnitOfWork(_factory.CreateFilledDbContext());
        var formulary = unit.ReaderFormulary.GetValueOrDefault(a => a.Name == "Name1", includeProperties:"TakenBooks.Author");
        
        
        Assert.That(formulary!.Name, Is.EqualTo("Name1"));
        Assert.That(formulary!.TakenBooks.First().Name, Is.EqualTo("Book1"));
        Assert.That(formulary!.TakenBooks.First().Author.Name, Is.EqualTo("Author1"));
    }

    [Test]
    public void GetAllFormularyWithBooksAndAuthors_IncludeParam_FormularyEnumWithBooksAndAuthors()
    {
        var unit = new UnitOfWork(_factory.CreateFilledDbContext());
        var formularies = unit.ReaderFormulary.GetAll(includeProperties: "TakenBooks.Author");
        Assert.That(formularies.Count(), Is.EqualTo(2));
        Assert.That(formularies.First().TakenBooks, Is.Not.Empty);
        Assert.That(formularies.First().TakenBooks.First().Author, Is.Not.Null);
    }

    [Test]
    public void GetAllBooks_WithoutIncludeParams_AllBooks()
    {
        var unit = new UnitOfWork(_factory.CreateFilledDbContext());
        var books = unit.Book.GetAll();
        Assert.That(books.Count(), Is.EqualTo(2));
    }
}