using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class WeightRepository(Supabase.Client supabase) : BaseRepository<WeightEntity>(supabase), IWeightRepository
{
    public async Task<IEnumerable<Weight>> Get20LatestWeights(Guid userId)
    {
        var result = await Supabase.From<WeightEntity>()
            .Filter("user_id", Constants.Operator.Equals, userId.ToString())
            .Order("created_at", Constants.Ordering.Descending) 
            .Limit(20)                                  
            .Get();
        
        return result.Models.Select(entity => entity.ToDomain());
    }
    
    public async Task<Weight> GetLatestWeight(Guid userId)
    {
        var result = await Supabase.From<WeightEntity>()
            .Filter("user_id", Constants.Operator.Equals, userId.ToString())
            .Order("created_at", Constants.Ordering.Descending) 
            .Limit(1)                                  
            .Get();
        
        return result.Models.FirstOrDefault().ToDomain();
    }
    
    public async Task LogLatestWeight(Weight weight)
    {
        WeightEntity entityToInsert = weight.ToEntity();

        await Insert(entityToInsert);
    }
}