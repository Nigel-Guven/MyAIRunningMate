using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Strava;
using Supabase;

namespace MyAIRunningMate.Database.Repository;

public class StravaResourceMapRepository(Client supabase) : BaseRepository<StravaGeoMapEntity>(supabase), IStravaResourceMapRepository
{
    public async Task<StravaGeoMapEntity> GetMapById(Guid mapId)
    {
        return await GetById(mapId);
    }
}