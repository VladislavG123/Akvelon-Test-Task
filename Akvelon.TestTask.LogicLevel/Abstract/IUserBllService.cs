using Akvelon.TestTask.LogicLevel.DTOs;

namespace Akvelon.TestTask.LogicLevel.Abstract;

public interface IUserBllService
{
    Task<string> SignIn(string login, string password);
    Task<string> SignUp(string login, string password);
    Task<UserDto> GetUserByToken(string jwtToken);
}