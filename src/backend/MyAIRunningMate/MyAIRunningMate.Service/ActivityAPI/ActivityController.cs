using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Garmin;
using MyAIRunningMate.Domain.Models.Activities;

namespace MyAIRunningMate.Service.ActivityAPI;

[ApiController]
[Route("api/activity")]
public class ActivityController : ControllerBase
{
    private readonly IActivityRepository _activityRepository;

    public ActivityController(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }
    
    [HttpGet("monthly")]
    public async Task<ActionResult<IEnumerable<ActivityDto>>> GetMonthlyActivities([FromQuery] int month, [FromQuery] int year)
    {
        var queryDate = new DateTime(year, month, 1);
        
        try
        {
            var activities = await _activityRepository.GetAllActivitiesByMonth(queryDate);
            return Ok(activities);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
}