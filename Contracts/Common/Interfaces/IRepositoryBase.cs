using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contracts.Common.Interfaces;

public interface IRepositoryBase<T, K> : IRepositoryQueryBase<T, K>
    where T : EntityBase<K>
{
    Task<K> CreateAsync(T entity);
    Task<IList<K>> CreateListAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task UpdateListAsync(IEnumerable<T> entities);
    Task DeleteAsync(T entity);
    Task DeleteListAsync(IEnumerable<T> entities);
    
    Task<int> SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task EndTransactionAsync();
    Task RollbackTransactionAsync();
}

public interface IRepositoryBase<T, K, TContext> : IRepositoryBase<T, K>
    where T : EntityBase<K>
    where TContext : DbContext
{
    
}
