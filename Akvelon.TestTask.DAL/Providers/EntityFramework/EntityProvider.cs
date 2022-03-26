using System.Linq.Expressions;
using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.DAL.Providers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.TestTask.DAL.Providers.EntityFramework;

public abstract class EntityProvider<TContext, TEntity, TId> : IProvider<TEntity, TId>
    // Model of database table
    where TEntity : Entity

    // Database context
    where TContext : DbContext

{
    private readonly TContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected EntityProvider(TContext context)
    {
        // database context
        this._context = context;

        // Now we get a DbSet from context by model 
        this._dbSet = context.Set<TEntity>();
    }

    public virtual async Task<List<TEntity>> GetAll(int take = Int32.MaxValue, int skip = 0)
    {
        // Gets all data from table
        // however point it as no tracking 
        return await _dbSet.AsNoTracking().OrderByDescending(entity => entity.CreationDate).Skip(skip).Take(take).ToListAsync();
    }

    public virtual async Task<TEntity> GetById(TId id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<List<TEntity>> Get(Func<TEntity, bool> predicate,
        int take = int.MaxValue, int skip = 0)
    {
        // Gets data which are acceded by a query
        return (await GetAll()).Where(predicate).OrderBy(entity => entity.CreationDate).Skip(skip).Take(take).ToList();
    }

    public virtual async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
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