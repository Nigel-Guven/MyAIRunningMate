using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class EventRepository(Supabase.Client supabase) : BaseRepository<EventEntity>(supabase), IEventRepository
{
    private readonly Supabase.Client _supabase = supabase;
    
    public async Task<IEnumerable<Event>> GetUpcomingEvents(int numberOfEvents)
    {
        var result = await _supabase.From<EventEntity>()
            .Order("event_date", Constants.Ordering.Ascending) 
            .Limit(numberOfEvents)                                  
            .Get();
        
        return result.Models.Select(entity => entity.ToDomain());
    }

    public async Task<Event?> GetEventById(Guid eventId)
    {
        var result = await _supabase
            .From<EventEntity>()
            .Where(x => x.EventId == eventId)
            .Limit(1)
            .Get();

        return result.Model?.ToDomain();
    }
}