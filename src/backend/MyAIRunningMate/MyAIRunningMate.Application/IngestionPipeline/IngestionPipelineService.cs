using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Application.Activities;
using MyAIRunningMate.Client.Python;
using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.ValueObjects;

namespace MyAIRunningMate.Application.IngestionPipeline;

public class IngestionPipelineService(
    IPythonApiClient pythonApiClient,
    IActivityService activityService)
    : IIngestionPipelineService
{
    public async Task<(Activity activity, int numberOfLaps, string status)> ProcessFitFileAsync(IFormFile file, Guid userId)
    {
        await using var stream = file.OpenReadStream();
        var (activity, laps) = await pythonApiClient.UploadFitFileAsync(stream, file.FileName, userId);

        var numberOfLaps = laps.Count();
        
        if (await activityService.CheckDuplicateAsync(activity.GarminActivityId, userId))
        {
            return (activity, numberOfLaps, IngestionStatus.ActivityExists);
        }
        
        await activityService.SaveActivityAndLaps(activity, laps, userId);

        return (activity, numberOfLaps, IngestionStatus.ActivityIngested);
        
    }
}