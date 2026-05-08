using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Events;

namespace MyAIRunningMate.Application.Events;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    
    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    public async Task<IEnumerable<EventEntity>> GetUpcomingFiveEvents(int numberOfEvents)
    {
        var entities = await _eventRepository.GetUpcomingEvents(numberOfEvents);

        return entities;
    }

    public async Task<EventEntity> GetPrimaryEvent(Guid eventId)
    {
        var entity = await _eventRepository.GetEventById(eventId);

        return entity;
    }
}