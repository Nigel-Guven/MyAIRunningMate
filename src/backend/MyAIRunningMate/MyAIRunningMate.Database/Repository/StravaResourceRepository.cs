using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Strava;
using Supabase.Postgrest;
using Client = Supabase.Client;

namespace MyAIRunningMate.Database.Repository;

public class StravaResourceRepository(Client supabase) : BaseRepository<StravaResourceEntity>(supabase), IStravaResourceRepository
{
    public async Task<StravaResourceEntity> GetStravaResourceById(Guid stravaId)
    {
        return await GetById(stravaId);
    }

    public async Task<IEnumerable<StravaResourceEntity>> GetAllStravaResourcesByIds(List<Guid> stravaIds)
    {
        if (!stravaIds.Any()) return new List<StravaResourceEntity>();

        var result = await _supabase
            .From<StravaResourceEntity>()
            .Filter("id", Constants.Operator.In, stravaIds)
            .Get();

        return result.Models;
    }
}
