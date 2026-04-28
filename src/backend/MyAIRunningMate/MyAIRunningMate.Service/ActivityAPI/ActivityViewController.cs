using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Models.DTO;

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
    public async Task<ActionResult<AggregateArtifactDto>> GetActivityView([FromQuery] Guid activityId)
    {
        if (activityId == Guid.Empty)
        {
            return BadRequest("Activity ID is required");
        }
        
        try
        {
            var aggregate = await _activityViewService.CreateAggregateActivityDto(activityId);

            return Ok(aggregate);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
}