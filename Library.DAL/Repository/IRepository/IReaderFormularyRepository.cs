using Library.DAL.Entities;

namespace Library.DAL.Repository.IRepository;

public interface IReaderFormularyRepository :IRepository<ReaderFormulary>
{
    void Update(ReaderFormulary readerFormulary);
}