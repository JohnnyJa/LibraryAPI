using System.Collections;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using MockQueryable.Moq;
using Moq;

namespace BLL.UnitTest.Mock;

public class MockFabric
{
    private readonly List<Author> _authorEntities;

    private readonly List<Subject> _subjectEntities;

    private readonly List<Book> _bookEntities;
    public MockFabric()
    {
        _authorEntities = new List<Author>()
        {
            new Author()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                Name = "Name",
                Surname = "Surname"
            },
            new Author()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Name = "Name2",
                Surname = "Surname2"
            }
        };
        _subjectEntities = new List<Subject>()
        {
            new Subject()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                Name = "Subject1",
            },
            new Subject()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Name = "Subject2"
            }
        };
        _bookEntities = new List<Book>()
        {
            new Book()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                Name = "Book1",
                ISBN = "1234567890",
                NumberOfCopies = 3,
                Author = _authorEntities[0],
                Subject = _subjectEntities[0],
                ReaderFormularies = new List<ReaderFormulary>()
            },
            new Book()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Name = "Book2",
                ISBN = "0123456789",
                NumberOfCopies = 2,
                Author = _authorEntities[1],
                Subject = _subjectEntities[1],
                ReaderFormularies = new List<ReaderFormulary>()
            }
        };
    }
        
    
    private Mock<IRepository<T>> GetRepository<T>(List<T> entities) where T : class
    {
        var repository = new Mock<IRepository<T>>();
        var mock = entities.BuildMock();
        repository.Setup(x => x.GetEnumerator()).Returns(mock.GetEnumerator());
        repository.Setup(x => x.Provider).Returns(mock.Provider);
        repository.Setup(x => x.Expression).Returns(mock.Expression);
        repository.Setup(x => x.ElementType).Returns(mock.ElementType);
        return repository;
    }
    
    public Mock<IRepository<Author>> GetAuthorRepository()
    {

        var repository = GetRepository(_authorEntities);
        repository.Setup(x => x.AddAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Author author, CancellationToken token) =>
            {
                _authorEntities.Add(author);
                return true;
            });
        repository.Setup(x => x.UpdateAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Author author, CancellationToken token) =>
            {
                _authorEntities[_authorEntities.FindIndex(x => x.Id == author.Id)] = author;
                return true;
            });
        repository.Setup(x => x.DeleteAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Author author, CancellationToken token) =>
            {
                _authorEntities.Remove(author);
                return true;
            });
        return repository;
    }
    
    public Mock<IRepository<Subject>> GetSubjectRepository()
    {

        var repository = GetRepository(_subjectEntities);
        repository.Setup(x => x.AddAsync(It.IsAny<Subject>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Subject subject, CancellationToken token) =>
            {
                _subjectEntities.Add(subject);
                return true;
            });
        repository.Setup(x => x.UpdateAsync(It.IsAny<Subject>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Subject subject, CancellationToken token) =>
            {
                _subjectEntities[_subjectEntities.FindIndex(x => x.Id == subject.Id)] = subject;
                return true;
            });
        repository.Setup(x => x.DeleteAsync(It.IsAny<Subject>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Subject subject, CancellationToken token) =>
            {
                _subjectEntities.Remove(subject);
                return true;
            });
        return repository;
    }

    public Mock<IRepository<Book>> GetBookRepository()
    {
        var repository = GetRepository(_bookEntities);
        repository.Setup(x => x.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Book book, CancellationToken token) =>
            {
                _bookEntities.Add(book);
                return true;
            });
        repository.Setup(x => x.UpdateAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Book book, CancellationToken token) =>
            {
                _bookEntities[_bookEntities.FindIndex(x => x.Id == book.Id)] = book;
                return true;
            });
        repository.Setup(x => x.DeleteAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Book book, CancellationToken token) =>
            {
                _bookEntities.Remove(book);
                return true;
            });
        return repository;
        
    }
}