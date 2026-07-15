using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.BestEfforts;
using MyAIRunningMate.Application.User;
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
}