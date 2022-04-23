using System.Security.Claims;
using Akvelon.TestTask.Web.Data.Apis;
using Microsoft.AspNetCore.Components.Authorization;

namespace Akvelon.TestTask.Web.Data.Services
{
    public class UserAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly UserApi _userApi;

        public UserAuthenticationStateProvider(ILocalStorageService localStorageService, UserApi userApi)
        {
            _localStorageService = localStorageService;
            _userApi = userApi;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AuthenticationState CreateAnonymous()
            {
                var anonymousIdentity = new ClaimsIdentity();
                var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
                return new AuthenticationState(anonymousPrincipal);
            }

            Console.WriteLine("Starting to authorize");
            
            var localClaims = await _localStorageService.GetAsync<UserClaims>(nameof(UserClaims));
            
            if (localClaims == null)
            {
                Console.WriteLine("claims are null");
                return CreateAnonymous();
            }

            if (string.IsNullOrWhiteSpace(localClaims.Token) || localClaims.ExpiredAt < DateTime.UtcNow)
            {
                Console.WriteLine("Token is empty");
                return CreateAnonymous();
            }

            try
            {
                var data = await _userApi.GetData(localClaims.Token);

                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, data.Login),
                    new(ClaimTypes.Expired, localClaims.ExpiredAt.ToLongDateString()),
                    new(ClaimTypes.Hash, localClaims.Token),
                };

                var identity = new ClaimsIdentity(claims, "Token");
                var principal = new ClaimsPrincipal(identity);

                Console.WriteLine("Auth-ed");
                return new AuthenticationState(principal);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateAnonymous();
            }
        }

        public void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }

    public class UserClaims
    {
        public string Login { get; set; }
        public string Token { get; set; }
        
        public DateTime ExpiredAt { get; set; }
    }
}