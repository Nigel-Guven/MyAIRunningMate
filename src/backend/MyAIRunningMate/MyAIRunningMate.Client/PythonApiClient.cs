using System.Net.Http.Json;
using MyAIRunningMate.Domain.Interfaces.Client;
using MyAIRunningMate.Domain.Models.Activities;

namespace MyAIRunningMate.Client;

public class PythonApiClient : IPythonApiClient
{
    private readonly HttpClient _httpClient;

    public PythonApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ActivityDto> UploadFitFileAsync(Stream fileStream, string fileName)
    {
        using var form = new MultipartFormDataContent();
        
        var streamContent = new StreamContent(fileStream);
        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

        form.Add(streamContent, "file", fileName);

        var response = await _httpClient.PostAsync("fit_file/upload", form);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<ActivityDto>() 
               ?? throw new Exception("Failed to deserialize Python response.");
    }
}