using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.DAL.Providers.Abstract;

namespace Akvelon.TestTask.DAL.Providers.Mongo;

public class MongoUserProvider: MongoProvider<UserEntity, Guid>, IUserProvider
{
    public MongoUserProvider(MongoDbContext context) : base(context)
    {
    }

    public async Task<UserEntity> GetByLogin(string login)
    {
        return await FirstOrDefault(x => x.Login.Equals(login))
                ?? throw new ArgumentException("No user with such login");
    }
}