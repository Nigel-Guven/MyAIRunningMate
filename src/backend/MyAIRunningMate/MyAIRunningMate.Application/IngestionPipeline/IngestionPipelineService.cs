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
    public async Task<(Activity activity, string status)> ProcessFitFileAsync(IFormFile file, Guid userId)
    {
        await using var stream = file.OpenReadStream();
        var aggregateArtifact = await pythonApiClient.UploadFitFileAsync(stream, file.FileName, userId);
        
        if (await activityService.CheckDuplicateAsync(aggregateArtifact.GarminActivity.GarminActivityId, userId))
        {
            return (aggregateArtifact.GarminActivity, IngestionStatus.ActivityExists);
        }
        
        await activityService.SaveActivity(aggregateArtifact.GarminActivity);
        await activityService.SaveLaps(aggregateArtifact.Laps);
        await activityService.SaveActivityMetrics(aggregateArtifact.GarminActivityMetrics);

        if (aggregateArtifact.BestEfforts != null)
        {
            await activityService.SaveBestEfforts(aggregateArtifact.BestEfforts);
        }
        
        if (aggregateArtifact.TimeSeriesRecords != null)
        {
            await activityService.SaveTimeSeriesRecords(aggregateArtifact.TimeSeriesRecords, aggregateArtifact.GarminActivity.ActivityId);
        }
        
        return (aggregateArtifact.GarminActivity, IngestionStatus.ActivityIngested);
        
    }
}