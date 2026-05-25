using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.TrainingPlan;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class TrainingPlanRepository(Supabase.Client supabase) : BaseRepository<TrainingPlanEntity>(supabase), ITrainingPlanRepository
{
    private readonly Supabase.Client _supabase = supabase;
    
    public async Task<TrainingPlanEntity?> GetByIdAsync(Guid trainingPlanId)
    {
        var response = await _supabase
            .From<TrainingPlanEntity>()
            .Filter("training_plan_id", Constants.Operator.Equals, trainingPlanId)
            .Single();

        return response;
    }

    public async Task<TrainingPlanEntity?> GetActivePlanForUserAsync(Guid userId, DateTime startOfMonth, DateTime endOfMonth)
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

        return response.Models.FirstOrDefault();
    }
    
    public async Task<IEnumerable<TrainingPlanEntity>> GetAllPlansForUserAsync(Guid userId)
    {
        var userIdString = userId.ToString().ToLower();

        var response = await _supabase
            .From<TrainingPlanEntity>()
            .Filter("user_id", Constants.Operator.Equals, userIdString)
            .Order("start_date", Constants.Ordering.Descending)
            .Get();

        return response.Models;
    }
}