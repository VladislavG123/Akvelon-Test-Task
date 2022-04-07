using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.DAL.Providers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.TestTask.DAL.Providers.EntityFramework;

public class EntityUserProvider: EntityProvider<ApplicationContext, UserEntity, Guid>, IUserProvider
{
    private readonly ApplicationContext _context;

    public EntityUserProvider(ApplicationContext context) : base(context)
    {
        _context = context;
    }


    public async Task<UserEntity> GetByLogin(string login)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Login.Equals(login))
               ?? throw new ArgumentException("User is not found");
    }
}