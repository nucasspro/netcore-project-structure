using System.Linq.Expressions;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;

namespace Contracts.Common.Interfaces;

public interface IRepositoryQueryBase<T, K> 
    where T: EntityBase<K>
{
    IQueryable<T> FindAll(bool trackingChanges = false);
    IQueryable<T> FindAll(bool trackingChanges = false, params Expression<Func<T, object>>[] includeProperties);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackingChanges = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackingChanges = false, params Expression<Func<T, object>>[] includeProperties);
    Task<T?> GetByIdAsync(K id);
    Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);
    
    Task<int> SaveChangesAsync();
}

public interface IRepositoryQueryBase<T, K, TContext> : IRepositoryQueryBase<T, K>
    where T : EntityBase<K>
    where TContext : DbContext
{
    
}
