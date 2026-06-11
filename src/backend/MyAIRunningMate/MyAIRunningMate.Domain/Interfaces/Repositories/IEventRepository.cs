using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetUpcomingEvents(int numberOfEvents);
    Task<Event?> GetEventById(Guid eventId);
}