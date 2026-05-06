using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Strava;

namespace MyAIRunningMate.Database.Repository;

public class StravaResourceMapRepository(Supabase.Client supabase) : BaseRepository<StravaGeomapEntity>(supabase), IStravaResourceMapRepository
{
    public async Task<StravaGeomapEntity> GetMapById(Guid mapId)
    {
        return await GetById(mapId);
    }
}