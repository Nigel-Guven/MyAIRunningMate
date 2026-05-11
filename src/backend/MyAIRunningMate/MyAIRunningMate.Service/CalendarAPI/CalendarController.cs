using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Calendar;
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
    private readonly IUserContext _userContext;
    
    public CalendarController(ICalendarService calendarService, IUserContext userContext)
    {
        _calendarService = calendarService;
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
}