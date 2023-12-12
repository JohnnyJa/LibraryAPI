using Library.DAL.Contexts;
using Library.DAL.Repository.IRepository;

namespace Library.DAL.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IBookRepository Book { get; }
    public IAuthorRepository Author { get; }
    public IReaderFormularyRepository ReaderFormulary { get; }
    public ISubjectRepository SubjectRepository { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Book = new BookRepository(_context);
        Author = new AuthorRepository(_context);
        ReaderFormulary = new ReaderFormularyRepository(_context);
        SubjectRepository = new SubjectRepository(_context);
    }
    
    public void Save()
    {
        _context.SaveChanges();
    }
}