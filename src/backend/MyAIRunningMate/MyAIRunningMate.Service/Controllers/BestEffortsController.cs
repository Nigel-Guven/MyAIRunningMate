using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.BestEfforts;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.BestEfforts.Requests;
using MyAIRunningMate.Contracts.BestEfforts.Responses;
using MyAIRunningMate.Service.Mappers;

namespace MyAIRunningMate.Service.Controllers;

[Authorize]
[ApiController]
[Route("api/best-efforts")]
public class BestEffortsController(IBestEffortService bestEffortService, IUserContext userContext)
    : ControllerBase
{
    [HttpGet("all-efforts")]
    public async Task<ActionResult<IEnumerable<BestEffortResponse>>> GetAllBestEfforts()
    {
        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        try
        {
            var bestEfforts = await bestEffortService.GetAllBestEfforts(userId);

            var dtos = bestEfforts.Select(bestEffort => bestEffort.ToBestEffortResponse());
            
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving best efforts: {ex.Message}");
        }
    }
    
    [HttpPost("update")]
    public async Task<ActionResult> UpdateBestEffort([FromBody] BestEffortRequest request)
    {
        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        try
        {
            await bestEffortService.UpdateBestEffort(request.DistanceLabel, request.NewPersonalRecordDate, request.NewPersonalRecordTime, userId);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error updating best effort: {ex.Message}");
        }
    }
}