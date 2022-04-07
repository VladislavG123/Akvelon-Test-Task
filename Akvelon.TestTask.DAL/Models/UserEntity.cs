namespace Akvelon.TestTask.DAL.Models;

public class UserEntity : Entity
{
    public string Login { get; set; }
    
    private string _passwordHash;
    public string PasswordHash
    {
        get => _passwordHash;
        set => _passwordHash = BCrypt.Net.BCrypt.HashPassword(value);
    }
    
    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, this.PasswordHash);
    }
}