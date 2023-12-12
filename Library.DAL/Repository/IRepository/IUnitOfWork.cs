using Library.DAL.Repository.IRepository;

namespace Library.DAL.Repository;

public interface IUnitOfWork
{
    IBookRepository Book { get; }
    IAuthorRepository Author { get; }
    IReaderFormularyRepository ReaderFormulary { get; }
    
    ISubjectRepository SubjectRepository { get; }

    void Save();
}