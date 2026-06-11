using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyAIRunningMate.Application.Activities;
using MyAIRunningMate.Application.LinkProvider;
using MyAIRunningMate.Application.Strava;
using MyAIRunningMate.Client.Python;
using MyAIRunningMate.Domain.Models.ViewObjects;

namespace MyAIRunningMate.Application.IngestionPipeline;

public class IngestionPipelineService : IIngestionPipelineService
{
    private readonly IPythonApiClient _pythonClient;
    private readonly ILinkProviderService _linkProviderService;
    private readonly IActivityService _activityService;
    private readonly IStravaResourceService _stravaResourceService;
    private readonly ILogger<IngestionPipelineService> _logger;
    
    public IngestionPipelineService(
        IPythonApiClient pythonApiClient, 
        ILinkProviderService linkProviderService,
        IActivityService activityService, 
        IStravaResourceService stravaResourceService,
        ILogger<IngestionPipelineService> logger)
    {
        _pythonClient = pythonApiClient;
        _linkProviderService = linkProviderService;
        _activityService = activityService;
        _stravaResourceService = stravaResourceService;
        _logger = logger;
    }
    
    public async Task<IngestionView> ProcessFitFileAsync(IFormFile file, Guid userId)
    {
        await using var stream = file.OpenReadStream();
        var response = await _pythonClient.UploadFitFileAsync(stream, file.FileName);

        var activity = response.ToActivity();
        
        if (await _activityService.CheckDuplicateAsync(activity.GarminActivityId, userId))
        {
            return activity.ToIngestionView();
        }
        
        try
        {
            var stravaResource = await _linkProviderService.FindAndLinkMatchAsync(activity);
            if (stravaResource == null)
            {
                _logger.LogError("Failed to link Strava match for activity {GarminActivityId}. Couldn't find a match.", activity.GarminActivityId);
                throw new InvalidOperationException("No Strava resource returned.");
            }
  
            var stravaEntityId  = await _stravaResourceService.SaveStravaResourceAndMap(stravaResource);
            await _activityService.SaveActivityAndLaps(activity, stravaEntityId, userId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Critical Failure: Failed to link Strava match for activity {GarminActivityId}.", activity.GarminActivityId);
            throw;
        }

        return activity.ToIngestionView();
        
    }
}