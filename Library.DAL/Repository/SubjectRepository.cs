using Library.DAL.Contexts;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;

namespace Library.DAL.Repository;

public class SubjectRepository : Repository<Subject>, ISubjectRepository
{
    public SubjectRepository(ApplicationDbContext db) : base(db)
    {
    }

    public void Update(Subject subject)
    {
        _db.Subjects.Update(subject);
    }
}