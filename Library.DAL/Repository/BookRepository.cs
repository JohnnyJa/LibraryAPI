using Library.DAL.Contexts;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Repository;

public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(ApplicationDBContext dbContext) : base(dbContext)
    {
        
    }

    public void Update(Book book)
    {
        _db.Books.Update(book);
    }
}