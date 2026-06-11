using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Repository;

public class StravaResourceMapRepository(Supabase.Client supabase) : BaseRepository<StravaGeomapEntity>(supabase), IStravaResourceMapRepository
{
    public async Task<StravaGeomap?> GetMapById(Guid mapId)
    {
        StravaGeomapEntity? entity = await GetById(mapId);

        return entity?.ToDomain();
    }
}