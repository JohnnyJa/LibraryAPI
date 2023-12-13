using System.Collections;
using System.Linq.Expressions;
using Library.DAL.Contexts;
using Library.DAL.Entities;
using Library.DAL.Entities.Base;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _table;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public Type ElementType => ((IQueryable<TEntity>)_table).ElementType;

    public Expression Expression => ((IQueryable<TEntity>)_table).Expression;

    public IQueryProvider Provider => ((IQueryable<TEntity>)_table).Provider;

    public IEnumerator<TEntity> GetEnumerator()
    {
        return ((IEnumerable<TEntity>)_table).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        return ((IAsyncEnumerable<TEntity>)_table).GetAsyncEnumerator(cancellationToken);
    }

    public async Task<bool> AddAsync(TEntity entity,
        CancellationToken cancellationToken = new())
    {
        await _table.AddAsync(entity, cancellationToken);
        return await SaveChangesAsync(cancellationToken) > 0;
    }

    public bool Add(TEntity entity)
    {
        _table.Add(entity);
        return SaveChanges() > 0;
    }

    public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = new())
    {
        await _table.AddRangeAsync(entities, cancellationToken);
        return  await SaveChangesAsync(cancellationToken) > 0;
    }

    public bool AddRange(IEnumerable<TEntity> entities)
    {
        _table.AddRange(entities);
        return SaveChanges() > 0;
    }

    public async Task<bool> UpdateAsync(TEntity entity,
        CancellationToken cancellationToken = new())
    {
        _table.Update(entity);
        return await SaveChangesAsync(cancellationToken) > 0;
    }

    public bool Update(TEntity entity)
    {
        _table.Update(entity);
        return SaveChanges() > 0;
    }

    public async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = new())
    {
        _table.UpdateRange(entities);
        return await SaveChangesAsync(cancellationToken) > 0;
    }

    public bool UpdateRange(IEnumerable<TEntity> entities)
    {
        _table.UpdateRange(entities);
        return SaveChanges() > 0;
    }

    public async Task<bool> DeleteAsync(TEntity entity,
        CancellationToken cancellationToken = new())
    {
        _table.Remove(entity);
        return await SaveChangesAsync(cancellationToken) > 0;
    }

    public bool Delete(TEntity entity)
    {
        _table.Remove(entity);
        return SaveChanges() > 0;
    }

    public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = new())
    {
        _table.RemoveRange(entities);
        return await SaveChangesAsync(cancellationToken) > 0;
    }

    public bool DeleteRange(IEnumerable<TEntity> entities)
    {
        _table.RemoveRange(entities);
        return SaveChanges() > 0;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}