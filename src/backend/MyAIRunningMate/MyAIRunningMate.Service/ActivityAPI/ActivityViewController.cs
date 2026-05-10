using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.AggregatePage;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Views;
using MyAIRunningMate.Service.ViewMappers;

namespace MyAIRunningMate.Service.ActivityAPI;

[Authorize]
[ApiController]
[Route("api/activity")]
public class ActivityViewController : ControllerBase
{
    private readonly IActivityViewService _activityViewService;
    private readonly IUserContext _userContext;
    
    public ActivityViewController(IActivityViewService activityViewService, IUserContext userContext)
    {
        _activityViewService = activityViewService;
        _userContext = userContext;
    }
    
    [HttpGet("aggregate")]
    public async Task<ActionResult<AggregateArtifactViewDto>> GetActivityView([FromQuery] Guid activityId)
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        if (activityId == Guid.Empty)
        {
            return BadRequest("Activity ID is required");
        }
        
        try
        {
            var aggregate = await _activityViewService.CreateAggregateActivity(activityId, userId);

            var dto = AggregateArtifactViewDtoMapper.ToAggregateArtifactViewDto(aggregate);
            
            return Ok(dto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
}