using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyAIRunningMate.Application.Activities;
using MyAIRunningMate.Application.Extensions;
using MyAIRunningMate.Application.LinkProvider;
using MyAIRunningMate.Application.Mappers;
using MyAIRunningMate.Client.Python;
using MyAIRunningMate.Contracts.Views;
using MyAIRunningMate.Domain.Interfaces.Services;

namespace MyAIRunningMate.Application.UserInterface;

public class IngestionPipelineService : IIngestionPipelineService
{
    private readonly IPythonApiClient _pythonClient;
    private readonly ILinkProviderService _linkProviderService;
    private readonly IActivityService _activityService;
    private readonly ILogger<IngestionPipelineService> _logger;
    
    public IngestionPipelineService(
        IPythonApiClient pythonApiClient, 
        ILinkProviderService linkProviderService,
        IActivityService activityService, 
        ILogger<IngestionPipelineService> logger)
    {
        _pythonClient = pythonApiClient;
        _linkProviderService = linkProviderService;
        _activityService = activityService;
        _logger = logger;
    }
    
    public async Task<IngestionViewDto> ProcessFitFileAsync(IFormFile file, Guid userId)
    {
        await using var stream = file.OpenReadStream();
        var response = await _pythonClient.UploadFitFileAsync(stream, file.FileName);

        var activity = response.ToActivity();
        //activity.UserId = userId;
        
        var activityExists = await _activityService.CheckDuplicateAsync(activity.GarminActivityId);

        if (activityExists)
        {
            return activity.ToIngestionView();
        }

        Guid? stravaResourceId = null;
        
        try
        {
            stravaResourceId = await _linkProviderService.FindAndLinkMatchAsync(activity);
            if (!GuidEx.IsGuid(stravaResourceId))
            {
                _logger.LogError("Failed to link Strava match for activity {GarminActivityId}. Received invalid Guid.", activity.GarminActivityId);
                throw new InvalidOperationException("Invalid Strava resource ID returned.");
            }
                
            await _activityService.SaveActivityAndLaps(activity, stravaResourceId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Critical Failure: Failed to link Strava match for activity {GarminActivityId}.", activity.GarminActivityId);
            throw;
        }

        return activity.ToIngestionView();
        
    }
}