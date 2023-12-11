using Library.DAL.Contexts;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;

namespace Library.DAL.Repository;

public class ReaderFormularyRepository : Repository<ReaderFormulary>, IReaderFormularyRepository
{
    public ReaderFormularyRepository(ApplicationDBContext db) : base(db)
    {
    }

    public void Update(ReaderFormulary readerFormulary)
    {
        _db.ReaderFormularies.Update(readerFormulary);
    }
}