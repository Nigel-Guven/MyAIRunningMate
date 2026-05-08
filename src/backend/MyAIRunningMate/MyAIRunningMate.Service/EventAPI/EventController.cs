using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Contracts.Views;

namespace MyAIRunningMate.Service.EventAPI;

[ApiController]
[Route("api/events")]
public class EventController : ControllerBase
{
    public EventController(){}
    
    [HttpGet("events")]
    public async Task<ActionResult<IEnumerable<EventViewDto>>> GetRacingEvents()
    {
        try
        {
            var eventViews = await _eventService.GetNextFiveUpcomingEvents();

            var dtos = eventViews.Select(eView => eView.ToEventViewDto());
            
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
    
    [HttpGet("single")]
    public async Task<ActionResult<IEnumerable<EventViewDto>>> GetPrimaryEvent()
    {
        const string mainEvent = "a080be41-370f-4ba0-9d29-6fa1db55072e";

        try
        {
            var eventViews = await _eventService.GetPrimaryEvent(mainEvent);

            var dtos = eventViews.Select(eView => eView.ToEventViewDto());
            
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
}