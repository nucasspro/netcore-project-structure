using System.Linq.Expressions;
using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Common;

public class RepositoryBase<T, K, TContext> : IRepositoryBase<T, K, TContext>
    where T : EntityBase<K>
    where TContext : DbContext
{
    private readonly TContext _dbContext;
    private readonly IUnitOfWork<TContext> _unitOfWork;
    private readonly DbSet<T> _dbSet;

    public RepositoryBase(TContext dbContext, IUnitOfWork<TContext> unitOfWork)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _dbSet = _dbContext.Set<T>();
    }

    public IQueryable<T> FindAll(bool trackingChanges = false)
    {
        return !trackingChanges
            ? _dbSet.AsNoTracking()
            : _dbSet;
    }

    public IQueryable<T> FindAll(bool trackingChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindAll(trackingChanges);
        items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        return items;
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackingChanges = false)
    {
        return !trackingChanges
            ? _dbSet.Where(expression).AsNoTracking()
            : _dbSet.Where(expression);
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackingChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindByCondition(expression, trackingChanges);
        items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        return items;
    }

    public async Task<T?> GetByIdAsync(K id)
    {
        return await FindByCondition(x => x.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties)
    {
        return await FindByCondition(x => x.Id.Equals(id), false, includeProperties).FirstOrDefaultAsync();
    }

    public async Task<K> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        return entities.Select(x => x.Id).ToList();
    }

    public Task UpdateAsync(T entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Unchanged)
            return Task.CompletedTask;

        var exist = _dbSet.Find(entity.Id);
        _dbContext.Entry(exist).CurrentValues.SetValues(entity);

        return Task.CompletedTask;
    }

    public Task UpdateListAsync(IEnumerable<T> entities)
    {
        return _dbSet.AddRangeAsync(entities);
    }

    public Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteListAsync(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return _dbContext.Database.BeginTransactionAsync();
    }

    public async Task EndTransactionAsync()
    {
        await SaveChangesAsync();
        await _dbContext.Database.CommitTransactionAsync();
    }

    public Task RollbackTransactionAsync()
    {
        return _dbContext.Database.RollbackTransactionAsync();
    }
    
    public Task<int> SaveChangesAsync()
    {
        return _unitOfWork.CommitAsync();
    }
}