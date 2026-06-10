using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Events;
using MyAIRunningMate.Contracts.Events.Responses;
using MyAIRunningMate.Service.ViewMappers;

namespace MyAIRunningMate.Service.Controllers;

[ApiController]
[Route("api/events")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }
    
    
    [HttpGet("upcoming")]
    public async Task<ActionResult<IEnumerable<EventViewResponse>>> GetRacingEvents()
    {
        const int numberOfEvents = 5;
        
        try
        {
            var eventEntities = await _eventService.GetUpcomingFiveEvents(numberOfEvents);

            var dtos = eventEntities.Select(e => e.ToEventViewDto());
            
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
    
    [HttpGet("primary")]
    public async Task<ActionResult<EventViewResponse>> GetPrimaryEvent()
    {
        Guid mainEvent = Guid.Parse("a080be41-370f-4ba0-9d29-6fa1db55072e");

        try
        {
            var entity = await _eventService.GetPrimaryEvent(mainEvent);

            var dto = entity.ToEventViewDto();
            
            return Ok(dto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
}