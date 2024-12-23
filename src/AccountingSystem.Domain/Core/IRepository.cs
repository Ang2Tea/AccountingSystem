using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AccountingSystem.Domain.Core
{
    public interface IRepository<TEntity, in TKey> 
        where TEntity : Entity<TKey> 
        where TKey :  IEquatable<TKey>
    {
        Task<IQueryable<TEntity>> GetQueryableAsync();
        Task CreateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<List<TEntity>> GetAllAsync();
        Task<IQueryable<TEntity>> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
    }
}