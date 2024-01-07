using System.Linq.Expressions;
using Library.DAL.Entities.Base;

namespace Library.DAL.Repository.IRepository;

public interface IRepository<TEntity> : IQueryable<TEntity>, IAsyncEnumerable<TEntity>
{

    Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = new());
    bool Add(TEntity entity);
    Task<bool> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = new());
    bool AddRange(IEnumerable<TEntity> entities);

    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = new());
    bool Update(TEntity entity);

    Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = new());
    bool UpdateRange(IEnumerable<TEntity> entities);

    Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = new());
    bool Delete(TEntity entity);

    Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = new());
    bool DeleteRange(IEnumerable<TEntity> entities);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    int SaveChanges();

}