using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Infrastructure;
using MyAIRunningMate.Domain.Models.Activities;

namespace MyAIRunningMate.Service.WeightAPI;

[ApiController]
[Route("api/weight")]
public class WeightController : ControllerBase
{
    private readonly IWeightRepository _weightRepository;

    public WeightController(IWeightRepository weightRepository)
    {
        _weightRepository = weightRepository;
    }
    
    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestSingleWeight([FromQuery] Guid userId)
    {
        if (userId == Guid.Empty) return BadRequest("Invalid User ID.");

        var result = await _weightRepository.GetLatestWeight(userId);
        return Ok(result.FirstOrDefault());
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetLatestMultipleWeights([FromQuery] Guid userId)
    {
        if (userId == Guid.Empty) return BadRequest("Invalid User ID.");

        var result = await _weightRepository.Get20LatestWeights(userId);
        return Ok(result);
    }
    
    [HttpPost("log_weight")]
    public async Task<IActionResult> LogWeight([FromBody] WeightDto request)
    {
        if (request.WeightInPounds <= 0) return BadRequest("Weight must be greater than 0.");
        if (request.UserId == Guid.Empty) return BadRequest("User ID is required.");

        try 
        {
            var entity = new WeightEntity
            {
                WeightId = request.WeightId == Guid.Empty ? Guid.NewGuid() : request.WeightId,
                WeightPounds = request.WeightInPounds,
                UserId = request.UserId,
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
    
    private WeightEntity MapWeightResourceDto(WeightDto dto)
    {
        if (dto == null) return null;
        
        return new WeightEntity()
        {
            WeightId = dto.WeightId,
            WeightPounds = dto.WeightInPounds,
            UserId = dto.UserId,
            CreatedAt =  dto.CreatedAt,
        };
    }
}