using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Application.Weights;
using MyAIRunningMate.Contracts.Weight.Requests;
using MyAIRunningMate.Contracts.Weight.Responses;
using MyAIRunningMate.Service.Mappers;

namespace MyAIRunningMate.Service.Controllers;

[Authorize]
[ApiController]
[Route("api/weight")]
public class WeightController(IWeightService weightService, IUserContext userContext) : ControllerBase
{
    [HttpGet("latest")]
    public async Task<ActionResult<WeightResponse>> GetLatestSingleWeight()
    {
        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var result = await weightService.GetLatestWeightAsync(userId);
        if(result is null) return NotFound("No weight has been recorded yet.");

        return Ok(result.ToResponse());
    }

    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<WeightResponse>>> GetLatestMultipleWeights()
    {
        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var results = await weightService.GetWeightHistoryAsync(userId);
        
        var dtos = results.Select(model => model.ToResponse());
        
        return Ok(dtos);
    }
    
    [HttpPost("log_weight")]
    public async Task<IActionResult> LogWeight([FromBody] LogWeightRequest request)
    {
        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        try 
        {
            var result = await weightService.LogWeightAsync(request.WeightInPounds, userId);
            
            var response = result.ToResponse();
            
            return CreatedAtAction(nameof(GetLatestSingleWeight), response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Database error: {ex.Message}");
        }
    }
}