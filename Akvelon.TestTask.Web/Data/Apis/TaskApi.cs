using System.Net.Http.Headers;
using System.Net.Http.Json;
using Akvelon.TestTask.Contracts.ViewModels;
using Akvelon.TestTask.Web.Data.Exceptions;

namespace Akvelon.TestTask.Web.Data.Apis;

public class TaskApi
{
    private readonly HttpClient _httpClient;
    private readonly string _urlBase = "api/tasks";

    public TaskApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TaskViewModel>> GetAll(string token, Guid? projectId,
        int take = Int32.MaxValue, int skip = 0)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var url = projectId is null
            ? _urlBase + $"?take={take}&skip={skip}"
            : _urlBase + $"?take={take}&skip={skip}&projectId={projectId}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new ArgumentException();
        }

        var responseData = await response.Content.ReadFromJsonAsync<List<TaskViewModel>>();

        return responseData;
    }

    public async Task<TaskViewModel> GetById(Guid id, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync(_urlBase + $"/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new ArgumentException(response.ReasonPhrase);
        }

        var data = await response.Content.ReadFromJsonAsync<TaskViewModel>();

        return data;
    }


    public async Task Create(TaskCreateViewModel parameter, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync(_urlBase, parameter);

        if (!response.IsSuccessStatusCode)
        {
            throw new UnsupportedHttpException(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task Edit(Guid id, TaskCreateViewModel parameter, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsJsonAsync(_urlBase + $"/{id}", parameter);

        if (!response.IsSuccessStatusCode)
        {
            throw new UnsupportedHttpException(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task AttachToProject(Guid id, Guid? projectId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PatchAsync(_urlBase + $"/{id}?projectId={projectId.ToString()}", null);

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