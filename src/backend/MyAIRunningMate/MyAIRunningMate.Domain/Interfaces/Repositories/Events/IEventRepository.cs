using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.Events;

public interface IEventRepository : IBaseRepository<EventEntity> 
{
    Task<IEnumerable<EventEntity>> GetUpcomingEvents(int numberOfEvents);
    Task<EventEntity> GetEventById(Guid eventId);
}