using System.Linq.Expressions;
using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.DAL.Providers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.TestTask.DAL.Providers.EntityFramework;

/// <summary>
/// Implementation of CRUD functional for Entity Framework
/// </summary>
/// <typeparam name="TContext">Database context</typeparam>
/// <typeparam name="TEntity">Model of database table</typeparam>
/// <typeparam name="TId"></typeparam>
public abstract class EntityProvider<TContext, TEntity, TId> : IProvider<TEntity, TId>
    where TEntity : Entity
    where TContext : DbContext

{
    private readonly TContext _context;
    private readonly DbSet<TEntity> _dbSet;

    protected EntityProvider(TContext context)
    {
        this._context = context;

        this._dbSet = context.Set<TEntity>();
    }

    public virtual async Task<List<TEntity>> GetAll(int take = Int32.MaxValue, int skip = 0)
    {
        return await _dbSet
            .AsNoTracking()
            .OrderByDescending(entity => entity.CreationDate)
            .Skip(skip).Take(take)
            .ToListAsync();
    }

    public virtual async Task<TEntity> GetById(TId id)
    {
        return await _dbSet.FirstAsync(x => x.Id.Equals(id));
    }

    public virtual async Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> predicate,
        int take = int.MaxValue, int skip = 0)
    {
        // Gets data which are acceded by a query
        return await _dbSet
            .Where(predicate)
            .OrderBy(entity => entity.CreationDate)
            .Skip(skip).Take(take)
            .ToListAsync();
    }

    public virtual async Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public virtual async Task Add(TEntity added)
    {
        await _dbSet.AddAsync(added);
        await _context.SaveChangesAsync();
    }

    public virtual async Task AddRange(IEnumerable<TEntity> added)
    {
        await _dbSet.AddRangeAsync(added);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Edit(TEntity edited)
    {
        _context.Entry(edited).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public virtual async Task Remove(TEntity removed)
    {
        _dbSet.Remove(removed);

        await _context.SaveChangesAsync();
    }
}