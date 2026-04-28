using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Weight;
using Supabase.Postgrest;
using Client = Supabase.Client;

namespace MyAIRunningMate.Database.Repository;

public class WeightRepository(Client supabase) : BaseRepository<WeightEntity>(supabase), IWeightRepository
{
    public async Task<IEnumerable<WeightEntity>> Get20LatestWeights(Guid userId)
    {
        var result = await _supabase.From<WeightEntity>()
            .Filter("user_id", Constants.Operator.Equals, userId)
            .Order("created_at", Constants.Ordering.Descending) 
            .Limit(20)                                  
            .Get();
        
        return result.Models;
    }

    public async Task<IEnumerable<WeightEntity>> GetLatestWeight(Guid userId)
    {
        var result = await _supabase.From<WeightEntity>()
            .Filter("user_id", Constants.Operator.Equals, userId)
            .Order("created_at", Constants.Ordering.Descending) 
            .Limit(1)                                  
            .Get();
        
        return result.Models;
    }

    public async Task LogLatestWeight(WeightEntity weight)
    {
        await Insert(weight);
    }
}