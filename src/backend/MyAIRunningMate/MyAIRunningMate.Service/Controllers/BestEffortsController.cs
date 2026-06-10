using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.BestEfforts;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.BestEffort;
using MyAIRunningMate.Contracts.Views;
using MyAIRunningMate.Service.ViewMappers;

namespace MyAIRunningMate.Service.Controllers;

[Authorize]
[ApiController]
[Route("api/best_efforts")]
public class BestEffortsController : ControllerBase
{
    private readonly IBestEffortService _bestEffortsService;
    private readonly IUserContext _userContext;
    
    public BestEffortsController(IBestEffortService bestEffortService, IUserContext userContext)
    {
        _bestEffortsService = bestEffortService;
        _userContext = userContext;
    }
    
    [HttpGet("all_efforts")]
    public async Task<ActionResult<IEnumerable<BestEffortViewDto>>> GetAllBestEfforts()
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        try
        {
            var bestEfforts = await _bestEffortsService.GetAllBestEfforts(userId);

            var dtos = bestEfforts.Select(bestEffort => bestEffort.ToBestEffortViewDto());
            
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
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        try
        {
            await _bestEffortsService.UpdateBestEffort(request.DistanceLabel, request.NewPersonalRecordDate, request.NewPersonalRecordTime, userId);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error updating best effort: {ex.Message}");
        }
    }
}