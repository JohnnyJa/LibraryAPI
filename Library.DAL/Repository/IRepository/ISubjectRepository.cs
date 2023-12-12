using Library.DAL.Entities;

namespace Library.DAL.Repository.IRepository;

public interface ISubjectRepository : IRepository<Subject>
{
    void Update(Subject subject);

}