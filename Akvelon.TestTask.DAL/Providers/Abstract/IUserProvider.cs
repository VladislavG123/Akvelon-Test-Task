using Akvelon.TestTask.DAL.Models;

namespace Akvelon.TestTask.DAL.Providers.Abstract;

/// <summary>
/// Provides database connection for UserEntity with CRUD functional
/// </summary>
public interface IUserProvider : IProvider<UserEntity, Guid>
{
    Task<UserEntity> GetByLogin(string login);
}