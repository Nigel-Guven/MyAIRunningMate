using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Events;

public class EventService(IEventRepository eventRepository) : IEventService
{
    public async Task<IEnumerable<Event>> GetUpcomingFiveEvents(int numberOfEvents)
    {
        var entities = await eventRepository.GetUpcomingEvents(numberOfEvents);

        return entities;
    }

    public async Task<Event?> GetPrimaryEvent(Guid eventId)
    {
        var entity = await eventRepository.GetEventById(eventId);

        return entity;
    }
}