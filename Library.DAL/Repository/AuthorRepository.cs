using Library.DAL.Contexts;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;

namespace Library.DAL.Repository;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository(ApplicationDBContext db) : base(db)
    {
    }

    public void Update(Author author)
    {
        _db.Authors.Update(author);
    }
}