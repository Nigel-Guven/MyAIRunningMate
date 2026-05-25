using System.Net.Http.Headers;
using System.Net.Http.Json;
using MyAIRunningMate.Client.Python.Requests;
using MyAIRunningMate.Client.Python.Responses;

namespace MyAIRunningMate.Client.Python;

public class PythonApiClient : IPythonApiClient
{
    private readonly HttpClient _httpClient;

    public PythonApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PythonApiActivityResponse> UploadFitFileAsync(Stream fileStream, string fileName)
    {
        try
        {
            using var form = new MultipartFormDataContent();
            
            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            form.Add(streamContent, "file", fileName);

            var response = await _httpClient.PostAsync("api/ingestion/process", form);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Python API returned {response.StatusCode}");
            }

            var result = await response.Content.ReadFromJsonAsync<PythonApiActivityResponse>();

            return result ?? throw new InvalidOperationException("Python API returned an empty response.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    
    public async Task<PythonApiTrainingPlanResponse> ProcessTrainingPlanRequisites(
        string primaryGoal, 
        int runningExperienceYears,
        string runningLevel,
        int trainingPlanLength, 
        string poolSize, 
        double poundWeight, 
        IEnumerable<PythonApiActivity> recentActivities)
    {
        try
        {
            var trainingPlanRequest = new TrainingPlanRequest()
            {
                PrimaryGoal = primaryGoal,
                RunningExperienceYears = runningExperienceYears,
                RunningLevel = runningLevel,
                TrainingPlanLength = trainingPlanLength,
                PoolSize = poolSize,
                WeightPounds = poundWeight,
                RecentActivities = recentActivities
            };
            
            var response = await _httpClient.PostAsJsonAsync("api/training_plan/draft", trainingPlanRequest);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"Python API returned {response.StatusCode}: {errorContent}");
            }

            var result = await response.Content.ReadFromJsonAsync<PythonApiTrainingPlanResponse>();

            return result ?? throw new InvalidOperationException("Python API returned an empty response.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}