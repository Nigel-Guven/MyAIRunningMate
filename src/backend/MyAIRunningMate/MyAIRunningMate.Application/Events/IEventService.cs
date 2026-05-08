using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Application.Events;

public interface IEventService
{
    Task<IEnumerable<EventEntity>> GetUpcomingFiveEvents(int numberOfEvents);
    Task<EventEntity> GetPrimaryEvent(Guid eventId);
}