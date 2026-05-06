using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Weight;
using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Weight;

namespace MyAIRunningMate.Service.WeightAPI;

[ApiController]
[Route("api/weight")]
public class WeightController : ControllerBase
{
    private readonly IWeightRepository _weightRepository;
    private readonly IUserContext _userContext;

    public WeightController(IWeightRepository weightRepository,  IUserContext userContext)
    {
        _weightRepository = weightRepository;
        _userContext =  userContext;
    }
    
    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestSingleWeight()
    {
        var userId = _userContext.GetUserId();
        
        if (userId == Guid.Empty) return BadRequest("User Id is required.");

        var result = await _weightRepository.GetLatestWeight(userId);
        return Ok(result.FirstOrDefault());
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetLatestMultipleWeights()
    {
        var userId = _userContext.GetUserId();
        
        if (userId == Guid.Empty) return BadRequest("User Id is required.");

        var result = await _weightRepository.Get20LatestWeights(userId);
        return Ok(result);
    }
    
    [HttpPost("log_weight")]
    public async Task<IActionResult> LogWeight([FromBody] WeightRequest request)
    {
        var userId = _userContext.GetUserId();
        
        if (request.WeightInPounds <= 0) return BadRequest("Weight must be greater than 0.");
        if (userId == Guid.Empty ) return BadRequest("User ID is required.");

        try 
        {
            var entity = new WeightEntity
            {
                WeightId = request.WeightId == Guid.Empty ? Guid.NewGuid() : request.WeightId,
                WeightPounds = request.WeightInPounds,
                UserId = userId,
                CreatedAt = request.CreatedAt == default ? DateTime.UtcNow : request.CreatedAt
            };

            await _weightRepository.LogLatestWeight(entity);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Database error: {ex.Message}");
        }
    }
}