using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Events;
using MyAIRunningMate.Contracts.Events.Responses;
using MyAIRunningMate.Service.Mappers;

namespace MyAIRunningMate.Service.Controllers;

[Authorize]
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
        const int numberOfEvents = 6;
        
        try
        {
            var eventEntities = await _eventService.GetUpcomingFiveEvents(numberOfEvents);

            var dtos = eventEntities.Select(e => e.ToEventResponse());
            
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
        Guid mainEvent = Guid.Parse("81e416ea-39af-4480-a738-1ef24ca38ee8");

        try
        {
            var entity = await _eventService.GetPrimaryEvent(mainEvent);

            if (entity != null)
            {
                var dto = entity.ToEventResponse();
            
                return Ok(dto);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving activities: {ex.Message}");
        }
    }
}