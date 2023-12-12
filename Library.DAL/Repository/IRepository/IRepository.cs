using System.Linq.Expressions;

namespace Library.DAL.Repository.IRepository;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll(string? includeProperties = null);
    T? GetValueOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);
    void Add(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
}