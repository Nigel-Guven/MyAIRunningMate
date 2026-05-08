using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Events;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class EventRepository(Supabase.Client supabase) : BaseRepository<EventEntity>(supabase), IEventRepository
{
    private readonly Supabase.Client _supabase = supabase;
    
    public async Task<IEnumerable<EventEntity>> GetUpcomingEvents(int numberOfEvents)
    {
        var result = await Supabase.From<EventEntity>()
            .Order("event_date", Constants.Ordering.Ascending) 
            .Limit(numberOfEvents)                                  
            .Get();

        return result.Models;
    }

    public async Task<EventEntity> GetEventById(Guid eventId)
    {
        var result = await _supabase
            .From<EventEntity>()
            .Where(x => x.EventId == eventId)
            .Limit(1)
            .Get();
        
        return result.Model;
    }
}