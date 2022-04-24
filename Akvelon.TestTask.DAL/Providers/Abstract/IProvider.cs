using System.Linq.Expressions;

namespace Akvelon.TestTask.DAL.Providers.Abstract;

/// <summary>
/// Abstract CRUD Provider
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TId"></typeparam>
public interface IProvider<TEntity, in TId>
    where TEntity : class
{
    Task<List<TEntity>> GetAll(int take = int.MaxValue, int skip = 0);
    Task<TEntity> GetById(TId id);
    Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, int take = int.MaxValue, int skip = 0);
    Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
    Task Add(TEntity added);
    Task AddRange(IEnumerable<TEntity> added);
    Task Edit(TEntity edited);
    Task Remove(TEntity removed);
}