using System.Net.Http.Headers;
using System.Net.Http.Json;
using MyAIRunningMate.Domain.Interfaces.Client;
using MyAIRunningMate.Domain.Providers.PythonFitApi.Responses;

namespace MyAIRunningMate.Client;

public class PythonApiClient : IPythonApiClient
{
    private readonly HttpClient _httpClient;

    public PythonApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PythonAPIActivityResponse> UploadFitFileAsync(Stream fileStream, string fileName)
    {
        try
        {
            using var form = new MultipartFormDataContent();
            
            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            form.Add(streamContent, "file", fileName);

            var response = await _httpClient.PostAsync("api/fit_file/upload", form);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Python API returned {response.StatusCode}");
            }

            var result = await response.Content.ReadFromJsonAsync<PythonAPIActivityResponse>();

            return result ?? throw new InvalidOperationException("Python API returned an empty response.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}