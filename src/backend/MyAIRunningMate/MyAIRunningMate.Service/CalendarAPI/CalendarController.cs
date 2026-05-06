using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Calendar;
using MyAIRunningMate.Contracts.Views;
using MyAIRunningMate.Service.ViewMappers;

namespace MyAIRunningMate.Service.CalendarAPI;

[ApiController]
[Route("api/calendar")]
public class CalendarController : ControllerBase
{
    private readonly ICalendarService _calendarService;
    
    public CalendarController(ICalendarService calendarService)
    {
        _calendarService = calendarService;
    }
    
    [HttpGet("monthly")]
    public async Task<ActionResult<IEnumerable<CalendarViewDto>>> GetMonthlyCalendarViews([FromQuery] int month, [FromQuery] int year)
    {
        var queryDate = new DateTime(year, month, 1);
        
        try
        {
            var calendarViews = await _calendarService.GetMonthlyCalendarViews(queryDate);

            var dtos = calendarViews.Select(cview => cview.ToCalendarViewDto());
            
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
}