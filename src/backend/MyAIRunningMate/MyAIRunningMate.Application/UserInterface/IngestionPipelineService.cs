using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Domain.Interfaces.Client;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Mappers;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Application.UserInterface;

public class IngestionPipelineService : IIngestionPipelineService
{
    private readonly IPythonApiClient _pythonClient;
    private readonly ILinkProviderService _linkProviderService;
    private readonly IActivityService _activityService;
    
    public IngestionPipelineService(
        IPythonApiClient pythonApiClient, 
        ILinkProviderService linkProviderService,
        IActivityService activityService)
    {
        _pythonClient = pythonApiClient;
        _linkProviderService = linkProviderService;
        _activityService = activityService;
    }
    
    public async Task<IngestionViewDto> ProcessFitFileAsync(IFormFile file, Guid userId)
    {
        await using var stream = file.OpenReadStream();
        var pythonResponse = await _pythonClient.UploadFitFileAsync(stream, file.FileName);
        
        var activityDto = pythonResponse.ToDto();

        var existing = await _activityService.CheckDuplicateAsync(activityDto.GarminActivityId);
        if (existing != null) return existing.ToIngestionView();


        Guid? stravaResourceId = null;
        try 
        {
            stravaResourceId = await _linkProviderService.FindAndLinkMatchAsync(activityDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        await _activityService.SaveActivityAndLaps(activityDto, stravaResourceId);

        return activityDto.ToIngestionView();
    }
}