using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Events;

namespace MyAIRunningMate.Database.Repository;

public class EventRepository(Supabase.Client supabase) : BaseRepository<EventEntity>(supabase), IEventRepository
{
    
}