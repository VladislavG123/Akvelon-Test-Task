using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.DAL.Providers.Abstract;
using Akvelon.TestTask.LogicLevel.Abstract;
using Akvelon.TestTask.LogicLevel.DTOs;
using Akvelon.TestTask.LogicLevel.Options;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Akvelon.TestTask.LogicLevel;

public class UserBllService : IUserBllService
{
    private readonly IUserProvider _userProvider;
    private readonly IMapper _mapper;
    private SecretOption SecretOptions { get; }

    public UserBllService(IUserProvider userProvider, IOptions<SecretOption> secretOptions, IMapper mapper)
    {
        _userProvider = userProvider;
        _mapper = mapper;
        SecretOptions = secretOptions.Value;
    }
    
    public async Task<string> SignIn(string login, string password)
    {
        // Find data by arguments
        var user = await _userProvider.GetByLogin(login);

        if (user.VerifyPassword(password))
            throw new ArgumentException("Incorrect password");

        return GenerateJwtToken(login);
    }

    public async Task<string> SignUp(string login, string password)
    {
        // Try to get a data from the arguments
        var user = await _userProvider.FirstOrDefault(x => x.Login == login);

        if (user is not null)
            throw new ArgumentException("This user is already exists");

        await _userProvider.Add(new UserEntity
        {
            Login = login,
            PasswordHash = password
        });

        return GenerateJwtToken(login);
    }

    private string GenerateJwtToken(string login)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SecretOptions.JwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, login),
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    /// <summary>
    ///    Token decryption
    /// </summary>
    /// <param name="token"></param>
    /// <exception cref="ArgumentException">throws when could not parse claims</exception>
    /// <returns>Owner's data</returns>
    private UserClaimsDto DecryptToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var tokenS = handler.ReadToken(token) as JwtSecurityToken;

        if (tokenS?.Claims is List<Claim> claims)
        {
            return new UserClaimsDto()
            {
                Login = claims[0].Value
            };
        }

        throw new ArgumentException();
    }

    /// <summary>
    /// Gets User by headers from Request
    /// Usage in controllers: 
    /// GetUserByHeaders(Request.Headers[HeaderNames.Authorization].ToArray()[0].Replace("Bearer ", ""))
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<UserDto> GetUserByToken(string token)
    {
        var user = await _userProvider.GetByLogin(DecryptToken(token).Login);

        return _mapper.Map<UserDto>(user);
    }
}