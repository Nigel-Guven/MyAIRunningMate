using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class TrainingPlanRepository(Supabase.Client supabase) : BaseRepository<TrainingPlanEntity>(supabase), ITrainingPlanRepository
{
    private readonly Supabase.Client _supabase = supabase;
    
    public async Task<TrainingPlan?> GetByIdAsync(Guid trainingPlanId)
    {
        var response = await _supabase
            .From<TrainingPlanEntity>()
            .Filter("id", Constants.Operator.Equals, trainingPlanId.ToString())
            .Single();

        return response?.ToDomain();
    }

    public async Task<TrainingPlan?> GetActivePlanForUserAsync(Guid userId, DateTime startOfMonth, DateTime endOfMonth)
    {
        var userIdString = userId.ToString().ToLower();
        var startIso = startOfMonth.ToString("yyyy-MM-dd");
        var endIso = endOfMonth.ToString("yyyy-MM-dd");
    
        var response = await _supabase
            .From<TrainingPlanEntity>()
            .Filter("user_id", Constants.Operator.Equals, userIdString)
            .Filter("start_date", Constants.Operator.LessThanOrEqual, endIso)
            .Filter("end_date", Constants.Operator.GreaterThanOrEqual, startIso)
            .Order("start_date", Constants.Ordering.Descending) 
            .Get();
        
        return response.Models.FirstOrDefault()?.ToDomain();
    }
    
    public async Task<IEnumerable<TrainingPlan>> GetAllPlansForUserAsync(Guid userId)
    {
        var response = await _supabase
            .From<TrainingPlanEntity>()
            .Filter("user_id", Constants.Operator.Equals, userId.ToString())
            .Get();

        return response.Models.Select(entity => entity.ToDomain());
    }
    
    public async Task InsertAsync(TrainingPlan trainingPlan)
    {
        TrainingPlanEntity entity = trainingPlan.ToEntity();
        await InsertAsync(entity);
    }
}