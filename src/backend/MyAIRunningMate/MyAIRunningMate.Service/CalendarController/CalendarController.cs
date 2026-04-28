using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Service.CalendarController;

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
    public async Task<ActionResult<IEnumerable<AggregateArtifactDto>>> GetMonthlyCalendarViews([FromQuery] int month, [FromQuery] int year)
    {
        var queryDate = new DateTime(year, month, 1);
        
        try
        {
            var aggregates = await _calendarService.GetMonthlyCalendarViews(queryDate);

            return Ok(aggregates);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
}