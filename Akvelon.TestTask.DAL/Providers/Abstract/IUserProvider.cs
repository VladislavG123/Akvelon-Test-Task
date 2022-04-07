using Akvelon.TestTask.DAL.Models;

namespace Akvelon.TestTask.DAL.Providers.Abstract;

public interface IUserProvider : IProvider<UserEntity, Guid>
{
    Task<UserEntity> GetByLogin(string login);
}