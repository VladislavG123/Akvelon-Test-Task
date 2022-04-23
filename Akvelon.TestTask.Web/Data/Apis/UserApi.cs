using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Akvelon.TestTask.Contracts.ViewModels;
using Akvelon.TestTask.Web.Data.Exceptions;

namespace Akvelon.TestTask.Web.Data.Apis;

public class UserApi
{
    private readonly HttpClient _httpClient;

    public UserApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Post request to api/identity
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundHttpException"></exception>
    /// <exception cref="BadRequestHttpException"></exception>
    /// <exception cref="UnsupportedHttpException"></exception>
    public async Task<string?> SignIn(UserAuthenticationViewModel parameter)
    {
        var response = await _httpClient.PostAsJsonAsync("api/user/sign-in", parameter);

        return response.StatusCode switch
        {
            HttpStatusCode.OK =>
                await response.Content.ReadAsStringAsync(),

            HttpStatusCode.NotFound =>
                throw new NotFoundHttpException(await response.Content.ReadAsStringAsync()),

            HttpStatusCode.BadRequest =>
                throw new BadRequestHttpException(await response.Content.ReadAsStringAsync()),

            _ => throw new UnsupportedHttpException($"Code: {(int) response.StatusCode}. " +
                                                    $"Message: {await response.Content.ReadAsStringAsync()}")
        };
    }
    
    
    public async Task<string?> SignUp(UserAuthenticationViewModel parameter)
    {
        var response = await _httpClient.PostAsJsonAsync("api/user/sign-up", parameter);

        return response.StatusCode switch
        {
            HttpStatusCode.OK =>
                await response.Content.ReadAsStringAsync(),

            HttpStatusCode.NotFound =>
                throw new NotFoundHttpException(await response.Content.ReadAsStringAsync()),

            HttpStatusCode.BadRequest =>
                throw new BadRequestHttpException(await response.Content.ReadAsStringAsync()),

            _ => throw new UnsupportedHttpException($"Code: {(int) response.StatusCode}. " +
                                                    $"Message: {await response.Content.ReadAsStringAsync()}")
        };
    }


    public async Task<UserViewModel?> GetData(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync("api/user");

        return response.StatusCode switch
        {
            HttpStatusCode.OK =>
                await response.Content.ReadFromJsonAsync<UserViewModel>(),

            HttpStatusCode.Unauthorized =>
                throw new UnauthorizedHttpException(await response.Content.ReadAsStringAsync()),

            _ => throw new UnsupportedHttpException(await response.Content.ReadAsStringAsync())
        };
    }
}
