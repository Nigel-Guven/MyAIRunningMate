using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class StravaResourceRepository(Supabase.Client supabase) : BaseRepository<StravaResourceEntity>(supabase), IStravaResourceRepository
{
    public async Task<StravaResource?> GetStravaResourceById(Guid stravaId)
    {
        StravaResourceEntity? entity = await GetById(stravaId);

        return entity?.ToDomain();
    }

    public async Task<IEnumerable<StravaResource>> GetAllStravaResourcesByIds(List<Guid>? stravaIds)
    {
        if (stravaIds == null || stravaIds.Count == 0) 
            return [];

        var result = await Supabase
            .From<StravaResourceEntity>()
            .Filter("id", Constants.Operator.In, stravaIds)
            .Get();
        
        return result.Models.Select(entity => entity.ToDomain());
    }
}