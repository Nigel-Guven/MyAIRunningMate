using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Strava;
using Supabase;

namespace MyAIRunningMate.Database.Repository;

public class StravaResourceRepository(Client supabase) : BaseRepository<StravaResourceEntity>(supabase), IStravaResourceRepository
{
    public async Task<StravaResourceEntity> StravaResourceById(Guid stravaId)
    {
        return await GetById(stravaId);
    }
}
