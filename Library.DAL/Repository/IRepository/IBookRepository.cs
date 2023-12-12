using Library.DAL.Entities;

namespace Library.DAL.Repository.IRepository;

public interface IBookRepository :IRepository<Book>
{
    void Update(Book book);
}