using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.TrainingPlan;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class TrainingPlanEventRepository(Supabase.Client supabase) : BaseRepository<TrainingPlanEventEntity>(supabase), ITrainingPlanEventRepository
{
    private readonly Supabase.Client _supabase = supabase;
    
    public async Task<IEnumerable<TrainingPlanEventEntity>> GetEventsForUserInDateRangeAsync(Guid trainingPlanId, DateTime start, DateTime end)
    {
        var planIdString = trainingPlanId.ToString().ToLower();
        var startIso = start.ToString("yyyy-MM-dd");
        var endIso = end.ToString("yyyy-MM-dd");
    
        var response = await _supabase
            .From<TrainingPlanEventEntity>()
            .Filter("training_plan_id", Constants.Operator.Equals, planIdString)
            .Filter("event_date", Constants.Operator.GreaterThanOrEqual, startIso)
            .Filter("event_date", Constants.Operator.LessThanOrEqual, endIso)
            .Order("event_date", Constants.Ordering.Ascending)
            .Get();

        return response.Models;
    }
    
    public async Task<IEnumerable<TrainingPlanEventEntity>> GetEventsByPlanIdAsync(Guid trainingPlanId)
    {
        var planIdString = trainingPlanId.ToString().ToLower();

        var response = await _supabase
            .From<TrainingPlanEventEntity>()
            .Filter("training_plan_id", Constants.Operator.Equals, planIdString)
            .Order("event_date", Constants.Ordering.Ascending)
            .Get();

        return response.Models;
    }
}