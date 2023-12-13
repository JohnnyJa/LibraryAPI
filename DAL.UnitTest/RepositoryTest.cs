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
        var repository = new Repository<Author>(_factory.CreateFilledDbContext());

        var author = repository.FirstOrDefault(a => a.Name == "Author1");
        Assert.That(author!.Name, Is.EqualTo("Author1"));
    }

    [Test]
    public void GetAuthor_NotExistName_AuthorIsNull()
    {
        var repository = new Repository<Author>(_factory.CreateFilledDbContext());

    
        var author = repository.FirstOrDefault(a => a.Name == "Author3");
        Assert.That(author, Is.Null);
    }
    
    [Test]
    public void GetAllAuthor_EmptyCondition_AuthorEnum()
    {
        var repository = new Repository<Author>(_factory.CreateFilledDbContext());
    
        var author = repository.ToList();
        Assert.That(author.Count(), Is.EqualTo(2));
    }
    
    [Test]
    public void AddAuthor_Author_Success()
    {
        var repository = new Repository<Author>(_factory.CreateFilledDbContext());

    
    
        var author = new Author()
        {
            Name = "Author4",
            Surname = "Author4"
        };
    
        repository.Add(author);
    
        var authors = repository.ToList();
        Assert.That(authors.Count(), Is.EqualTo(3));
    }
    
    [Test]
    public void DeleteAuthor_AuthorExist_Success()
    {
        var repository = new Repository<Author>(_factory.CreateFilledDbContext());
        
        var author = repository.FirstOrDefault(a => a.Surname == "Author1");
    
        repository.Delete(author!);
    
        var authors = repository.ToList();
        Assert.That(authors.Count(), Is.EqualTo(1));
    }
    
    [Test]
    public void DeleteRangeAuthor_AuthorList_Success()
    {
        var repository = new Repository<Author>(_factory.CreateFilledDbContext());
    
    
        var authorsBefore = repository.ToList();
    
        repository.DeleteRange(authorsBefore);
    
        var authorsAfter = repository.ToList();
        Assert.That(authorsAfter.Count(), Is.EqualTo(0));
    }
    
    [Test]
    public void DeleteAuthor_AuthorNotExist_Error()
    {
        var repository = new Repository<Author>(_factory.CreateFilledDbContext());

    
    
        var author = new Author
        {
            Name = "1",
            Surname = "q"
        };
    
        Assert.Throws<DbUpdateConcurrencyException>(() =>
        {
            repository.Delete(author);
    
        });
        
        var authors = repository.ToList();
        Assert.That(authors.Count(), Is.EqualTo(2));
    }
    
    [Test]
    public void UpdateAuthor_AuthorExist_Success()
    {
        var repository = new Repository<Author>(_factory.CreateFilledDbContext());
    
    
        var author = repository.FirstOrDefault(a => a.Surname == "Author1");
    
        author!.Name = "Author3";
        author.Surname = "author3";
        
        repository.Update(author);
    
        author = repository.FirstOrDefault(a => a.Name == "Author3");
        Assert.That(author!.Surname, Is.EqualTo("author3"));
    }
    
    [Test]
    public void GetFormularyWithBooksAndAuthors_FormularyExistWithIncludeParam_FormularyWithBooksAndAuthors()
    {
        var repository = new Repository<Author>(_factory.CreateFilledDbContext());
        var author = repository.Include(a => a.Books).ThenInclude(b=>b.ReaderFormularies).FirstOrDefault(a => a.Name == "Author1");
        
        
        Assert.That(author!.Name, Is.EqualTo("Author1"));
        Assert.That(author.Books, Is.Not.Empty);
        Assert.That(author.Books.FirstOrDefault().ReaderFormularies, Is.Not.Empty);
    }

}