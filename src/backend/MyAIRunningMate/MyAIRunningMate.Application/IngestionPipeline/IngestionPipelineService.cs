using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Application.Activities;
using MyAIRunningMate.Application.LinkProvider;
using MyAIRunningMate.Application.Strava;
using MyAIRunningMate.Client.Python;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.IngestionPipeline;

public class IngestionPipelineService(
    IPythonApiClient pythonApiClient,
    ILinkProviderService linkProviderService,
    IActivityService activityService,
    IStravaResourceService stravaResourceService)
    : IIngestionPipelineService
{
    public async Task<(Activity activity, int numberOfLaps)> ProcessFitFileAsync(IFormFile file, Guid userId)
    {
        await using var stream = file.OpenReadStream();
        var (activity, laps) = await pythonApiClient.UploadFitFileAsync(stream, file.FileName, userId);

        var numberOfLaps = laps.ToList().Count;
        
        if (await activityService.CheckDuplicateAsync(activity.GarminActivityId, userId))
        {
            return (activity, numberOfLaps);
        }

        var stravaResource = await linkProviderService.FindAndLinkMatchAsync(activity);

        var stravaEntityId  = await stravaResourceService.SaveStravaResourceAndMap(stravaResource);
        await activityService.SaveActivityAndLaps(activity, laps, stravaEntityId, userId);

        return (activity, numberOfLaps);
        
    }
}