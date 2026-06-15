using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.AggregatePage;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Aggregates.Responses;
using MyAIRunningMate.Service.Mappers;

namespace MyAIRunningMate.Service.Controllers;

[Authorize]
[ApiController]
[Route("api/activity")]
public class ActivityViewController(IActivityViewService activityViewService, IUserContext userContext)
    : ControllerBase
{
    [HttpGet("aggregate")]
    public async Task<ActionResult<AggregateArtifactResponse>> GetActivityView([FromQuery] Guid activityId)
    {
        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        if (activityId == Guid.Empty)
        {
            return BadRequest("Activity ID is required");
        }
        
        try
        {
            var aggregate = await activityViewService.CreateAggregateActivity(activityId, userId);

            var dto = aggregate.ToAggregateArtifactDto();
            
            return Ok(dto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
}