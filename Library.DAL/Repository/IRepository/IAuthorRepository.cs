using Library.DAL.Entities;

namespace Library.DAL.Repository.IRepository;

public interface IAuthorRepository :IRepository<Author>
{
    void Update(Author author);
}