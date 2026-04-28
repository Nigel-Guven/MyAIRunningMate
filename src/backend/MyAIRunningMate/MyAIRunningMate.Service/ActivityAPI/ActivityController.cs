using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Service.ActivityAPI;

[ApiController]
[Route("api/activity")]
public class ActivityController : ControllerBase
{

    private readonly IAggregatorMapper _aggregatorMapper;

    public ActivityController(IAggregatorMapper aggregatorMapper)
    {
        _aggregatorMapper = aggregatorMapper;
    }
    
    [HttpGet("monthly")]
    public async Task<ActionResult<IEnumerable<AggregateArtifactDto>>> GetMonthlyActivities([FromQuery] int month, [FromQuery] int year)
    {
        var queryDate = new DateTime(year, month, 1);
        
        try
        {
            var aggregates = await _aggregatorMapper.GetMonthlyAggregates(queryDate);

            return Ok(aggregates);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
}