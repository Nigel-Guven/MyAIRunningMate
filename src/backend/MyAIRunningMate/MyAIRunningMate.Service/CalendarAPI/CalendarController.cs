using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Calendar;
using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Application.TrainingPlans;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Views;
using MyAIRunningMate.Service.ViewMappers;

namespace MyAIRunningMate.Service.CalendarAPI;

[Authorize]
[ApiController]
[Route("api/calendar")]
public class CalendarController : ControllerBase
{
    private readonly ICalendarService _calendarService;
    private readonly ITrainingPlanService _trainingPlanService;
    private readonly IUserContext _userContext;
    
    public CalendarController(ICalendarService calendarService, ITrainingPlanService trainingPlanService, IUserContext userContext)
    {
        _calendarService = calendarService;
        _trainingPlanService = trainingPlanService;
        _userContext = userContext;
    }
    
    [HttpGet("display")]
    public async Task<ActionResult<IEnumerable<CalendarViewDto>>> GetMonthlyCalendarViews([FromQuery] int month, [FromQuery] int year)
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        var queryDate = new DateTime(year, month, 1);
        
        try
        {
            var calendarViews = await _calendarService.GetMonthlyCalendarViews(queryDate, userId);

            var dtos = calendarViews.Select(view => view.ToCalendarViewDto());
            
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
    
    [HttpGet("training-plan")]
    public async Task<ActionResult<TrainingPlanView>> GetActiveTrainingPlanForMonth([FromQuery] int month, [FromQuery] int year)
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var startOfMonth = new DateTime(year, month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

        try
        {
            var plan = await _trainingPlanService.GetActivePlanForUserAsync(userId, startOfMonth, endOfMonth);
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