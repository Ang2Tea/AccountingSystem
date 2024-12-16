using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingSystem.Domain.Core
{
    public interface IRepository<TEntity, in TKey> 
        where TEntity : Entity<TKey> 
        where TKey :  IEquatable<TKey>
    {
        Task CreateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<List<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
    }
}