using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AccountingSystem.Domain.Core;
using AccountingSystem.EntityFramework.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.EntityFramework.Repositories;

public class EfRepository<TEntity, TKey>(
    AccountingSystemDbContext context
    ) : IRepository<TEntity, TKey> 
    where TEntity : Entity<TKey> 
    where TKey :  IEquatable<TKey>
{
    public Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        return Task.FromResult(context.Set<TEntity>().AsQueryable());
    }

    public async Task CreateAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<TEntity> GetByIdAsync(TKey id)
    {
        var result = await context
            .Set<TEntity>()
            .SingleOrDefaultAsync(e => e.Id.Equals(id));
        
        return result;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        var query = context
            .Set<TEntity>()
            .AsQueryable();
        
        return await query.ToListAsync();
    }

    public Task<IQueryable<TEntity>> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors)
    {
        var result = context.Set<TEntity>().AsQueryable();

        foreach (var propertySelector in propertySelectors)
        {
            result = result.Include(propertySelector);
        }
        
        return Task.FromResult(result);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TKey id)
    {
        var entity = await GetByIdAsync(id);
        context.Set<TEntity>().Remove(entity);
        
        await context.SaveChangesAsync();
    }
}