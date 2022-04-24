using System.Net.Http.Headers;
using System.Net.Http.Json;
using Akvelon.TestTask.Contracts.ViewModels;
using Akvelon.TestTask.Web.Data.Exceptions;

namespace Akvelon.TestTask.Web.Data.Apis;

public class ProjectApi
{
    private readonly HttpClient _httpClient;
    private readonly string _urlBase = "api/projects";

    public ProjectApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProjectViewModel>> GetAll(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync(_urlBase);

        if (!response.IsSuccessStatusCode)
        {
            throw new ArgumentException();
        }

        var responseData = await response.Content.ReadFromJsonAsync<List<ProjectViewModel>>();

        return responseData;
    }

    public async Task<ProjectViewModel> GetById(Guid id, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync(_urlBase + $"/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new ArgumentException(response.ReasonPhrase);
        }

        var data = await response.Content.ReadFromJsonAsync<ProjectViewModel>();

        return data;
    }

    public async Task Create(ProjectCreationViewModel parameter, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync(_urlBase, parameter);

        if (!response.IsSuccessStatusCode)
        {
            throw new UnsupportedHttpException(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task Edit(Guid id, ProjectEditViewModel parameter, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsJsonAsync(_urlBase + $"/{id}", parameter);

        if (!response.IsSuccessStatusCode)
        {
            throw new UnsupportedHttpException(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task Delete(Guid id, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.DeleteAsync(_urlBase + $"/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new UnsupportedHttpException(await response.Content.ReadAsStringAsync());
        }
    }
}