namespace Akvelon.TestTask.Web.Data.Services;

public class UserClaims
{
    public string Login { get; set; }
    public string Token { get; set; }

    public DateTime ExpiredAt { get; set; }
}