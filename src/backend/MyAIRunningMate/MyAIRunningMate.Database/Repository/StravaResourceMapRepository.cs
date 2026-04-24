using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Strava;
using Supabase;

namespace MyAIRunningMate.Database.Repository;

public class StravaResourceMapRepository(Client supabase) : BaseRepository<StravaResourceMapEntity>(supabase), IStravaResourceMapRepository
{
    public async Task<StravaResourceMapEntity> GetMapById(Guid mapId)
    {
        return await GetById(mapId);
    }
}