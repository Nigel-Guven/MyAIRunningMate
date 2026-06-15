using System.Net.Http.Headers;
using System.Net.Http.Json;
using MyAIRunningMate.Client.Geocoder;
using MyAIRunningMate.Client.Geocoder.Extensions;
using MyAIRunningMate.Client.Python.Requests;
using MyAIRunningMate.Client.Python.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Client.Python;

public class PythonApiClient(HttpClient httpClient, IGeocodeClient geocodeClient) : IPythonApiClient
{
    public async Task<(Activity Activity, IEnumerable<Lap> Laps)> UploadFitFileAsync(Stream fileStream, string fileName, Guid userId)
    {
        using var form = new MultipartFormDataContent();
        using var streamContent = new StreamContent(fileStream);
        
        streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        form.Add(streamContent, "file", fileName);

        var response = await httpClient.PostAsync("api/ingestion/process", form);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Python Ingestion Service returned Status {response.StatusCode}: {errorContent}");
        }

        var result = await response.Content.ReadFromJsonAsync<PythonApiActivityResponse>();
        
        if (result == null)
        {
            throw new InvalidOperationException("Python Ingestion Service returned an empty or invalid payload response.");
        }
        
        string? mapPolyline = null;
        string? resolvedLocation = null;
        
        if (result.PoolLength == null)
        {
            var validCoordinates = result.TimeSeries
                .Where(ts => ts.Latitude.HasValue && ts.Longitude.HasValue)
                .Select(ts => (ts.Latitude!.Value, ts.Longitude!.Value))
                .ToList();

            if (validCoordinates.Count > 0)
            {
                mapPolyline = PolylineFactory.Encode(validCoordinates);

                var (firstLat, firstLng) = validCoordinates.First();
                resolvedLocation = await geocodeClient.GetReadableLocationAsync(firstLat, firstLng);
            }
        }
        else if (result.PoolLength != null)
        {
            resolvedLocation = $"Indoor Pool ({result.PoolLength}m)";
        }
        
        return result?.ToDomain(userId, mapPolyline, resolvedLocation) ?? throw new InvalidOperationException("Python Ingestion Service returned an empty or invalid payload response.");
    }
    
    public async Task<(TrainingPlan TrainingPlan, IEnumerable<TrainingPlanEvent> Events)> GenerateTrainingPlanAsync(
        string primaryGoal,
        int runningExperienceYears,
        string runningLevel,
        int trainingPlanLength,
        string poolSize,
        double userWeight,
        IEnumerable<Activity> activityHistory,
        Guid userId)
    {

        var formattedActivities = activityHistory.Select(act 
            => new PythonApiActivity(
                ExerciseType: act.ExerciseType,
                StartTime: act.StartTime,
                DurationSeconds: act.DurationSeconds,
                DistanceMetres: act.DistanceMetres,
                AverageHeartRate: act.AverageHeartRate,
                MaxHeartRate: act.MaxHeartRate,
                TotalElevationGain: act.TotalElevationGain,
                RawPaceSecondsPerMetre: act.RawPaceSecondsPerMetre,
                TrainingEffect: act.TrainingEffect
                ));
        
        var requestPayload = new PythonApiTrainingPlanRequest(
            PrimaryGoal: primaryGoal,
            RunningExperienceYears: runningExperienceYears,
            RunningLevel: runningLevel,
            TrainingPlanLength: trainingPlanLength,
            PoolSize: poolSize,
            UserWeight: userWeight,
            RecentActivities: formattedActivities);
        
        var response = await httpClient.PostAsJsonAsync("api/training_plan/draft", requestPayload);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Python Core AI Planner returned Status {response.StatusCode}: {errorContent}");
        }

        var result = await response.Content.ReadFromJsonAsync<PythonApiTrainingPlanResponse>();
        return result?.ToDomain(userId) ?? throw new InvalidOperationException("Python Core AI Planner returned an empty or invalid plan payload response.");
    }
}