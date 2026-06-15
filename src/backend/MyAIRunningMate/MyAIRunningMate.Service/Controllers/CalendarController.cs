using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Calendar;
using MyAIRunningMate.Application.TrainingPlans;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Calendar.Responses;
using MyAIRunningMate.Contracts.Nexus.Responses;
using MyAIRunningMate.Service.Mappers;

namespace MyAIRunningMate.Service.Controllers;

[Authorize]
[ApiController]
[Route("api/calendar")]
public class CalendarController(
    ICalendarService calendarService,
    ITrainingPlanService trainingPlanService,
    IUserContext userContext)
    : ControllerBase
{
    [HttpGet("display")]
    public async Task<ActionResult<IEnumerable<CalendarViewResponse>>> GetMonthlyCalendarViews([FromQuery] int month, [FromQuery] int year)
    {
        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        var queryDate = new DateTime(year, month, 1);
        
        try
        {
            var activities = await calendarService.GetMonthlyCalendarViews(queryDate, userId);

            var dtos = activities.Select(act => act.ToCalendarViewResponse());
            
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
    
    [HttpGet("training-plan")]
    public async Task<ActionResult<TrainingPlanViewResponse>> GetActiveTrainingPlanForMonth([FromQuery] int month, [FromQuery] int year)
    {
        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var startOfMonth = new DateTime(year, month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

        try
        {
            var plan = await trainingPlanService.GetActivePlanForUserAsync(userId, startOfMonth, endOfMonth);
            if (plan == null)
            {
                return NotFound("No active training plan found matching this timeline window.");
            }
            return Ok(plan);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving scheduled layout: {ex.Message}");
        }
    }
}