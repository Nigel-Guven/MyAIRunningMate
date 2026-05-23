using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.TrainingPlan;

namespace MyAIRunningMate.Database.Repository;

public class TrainingPlanRepository(Supabase.Client supabase) : BaseRepository<TrainingPlanEntity>(supabase), ITrainingPlanRepository
{
    
}