using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.BestEfforts;

namespace MyAIRunningMate.Database.Repository;

public class BestEffortsRepository(Supabase.Client supabase) : BaseRepository<BestEffortEntity>(supabase), IBestEffortsRepository
{
    
}