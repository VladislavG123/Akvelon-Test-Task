using System.Linq.Expressions;
using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.DAL.Providers.Abstract;
using MongoDB.Driver;

namespace Akvelon.TestTask.DAL.Providers.Mongo;

/// <summary>
/// Implementation of CRUD functional for Mongo Driver
/// </summary>
/// <typeparam name="TEntity">Model of database table</typeparam>
/// <typeparam name="TId"></typeparam>
public abstract class MongoProvider<TEntity, TId> : IProvider<TEntity, TId> where TEntity : Entity
{
    private readonly MongoDbContext _context;

    private IMongoCollection<TEntity>? _collection;

    public MongoProvider(MongoDbContext context)
    {
        _context = context;
        _collection = null;
    }

    /// <summary>
    /// Returns existed collection or creates new if does not exists
    /// Singleton Realization 
    /// </summary>
    /// <returns></returns>
    protected async Task<IMongoCollection<TEntity>> GetCollection()
    {
        if (_collection is not null) return _collection;

        await _context.CreateCollectionIfNotExists(typeof(TEntity).Name);
        _collection = await _context.GetCollection<TEntity>(typeof(TEntity).Name);

        return _collection;
    }

    public virtual async Task<List<TEntity>> GetAll(int take = Int32.MaxValue, int skip = 0)
    {
        var collection = await GetCollection();

        return await collection.Find(_ => true).Skip(skip).Limit(take).ToListAsync();
    }

    public virtual async Task<TEntity> GetById(TId id)
    {
        var collection = await GetCollection();

        return await collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public virtual async Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, int take = Int32.MaxValue,
        int skip = 0)
    {
        var collection = await GetCollection();

        return await collection.Find(predicate).Skip(skip).Limit(take).ToListAsync();
    }

    public virtual async Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        return (await Get(predicate, 1)).FirstOrDefault();
    }

    public virtual async Task Add(TEntity added)
    {
        var collection = await GetCollection();

        await collection.InsertOneAsync(added);
    }

    public virtual async Task AddRange(IEnumerable<TEntity> added)
    {
        var collection = await GetCollection();

        await collection.InsertManyAsync(added);
    }

    public virtual async Task Edit(TEntity edited)
    {
        var collection = await GetCollection();

        await collection.ReplaceOneAsync(x => x.Id == edited.Id, edited);
    }

    public virtual async Task Remove(TEntity removed)
    {
        var collection = await GetCollection();

        await collection.DeleteOneAsync(x => x.Id == removed.Id);
    }
}