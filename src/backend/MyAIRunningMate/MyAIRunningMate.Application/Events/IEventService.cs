using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Events;

public interface IEventService
{
    Task<IEnumerable<Event>> GetUpcomingFiveEvents(int numberOfEvents);
    Task<Event?> GetPrimaryEvent(Guid eventId);
}