using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Contracts.Views;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Service.ViewMappers;

namespace MyAIRunningMate.Service.ActivityAPI;

[ApiController]
[Route("api/activity")]
public class ActivityViewController : ControllerBase
{
    private readonly IActivityViewService _activityViewService;
    
    public ActivityViewController(IActivityViewService activityViewService)
    {
        _activityViewService = activityViewService;
    }
    
    [HttpGet("monthly")]
    public async Task<ActionResult<AggregateArtifactViewDto>> GetActivityView([FromQuery] Guid activityId)
    {
        if (activityId == Guid.Empty)
        {
            return BadRequest("Activity ID is required");
        }
        
        try
        {
            var aggregate = await _activityViewService.CreateAggregateActivity(activityId);

            var dto = AggregateArtifactViewDtoMapper.ToAggregateArtifactViewDto(aggregate);
            
            return Ok(dto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
}