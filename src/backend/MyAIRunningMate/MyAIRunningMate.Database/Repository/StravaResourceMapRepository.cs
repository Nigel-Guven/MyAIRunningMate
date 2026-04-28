using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Strava;
using Supabase;

namespace MyAIRunningMate.Database.Repository;

public class StravaResourceMapRepository(Client supabase) : BaseRepository<StravaGeomapEntity>(supabase), IStravaResourceMapRepository
{
    public async Task<StravaGeomapEntity> GetMapById(Guid mapId)
    {
        return await GetById(mapId);
    }
}