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
    private readonly List<Author> authorEntities = new List<Author>()
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
        var authorEntities = new List<Author>()
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

        var repository = GetRepository(authorEntities);
        repository.Setup(x => x.AddAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Author author, CancellationToken token) =>
            {
                authorEntities.Add(author);
                return true;
            });
        repository.Setup(x => x.UpdateAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Author author, CancellationToken token) =>
            {
                authorEntities[authorEntities.FindIndex(x => x.Id == author.Id)] = author;
                return true;
            });
        repository.Setup(x => x.DeleteAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Author author, CancellationToken token) =>
            {
                authorEntities.Remove(author);
                return true;
            });
        return repository;
    }
}