using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.TrainingPlan;

namespace MyAIRunningMate.Database.Repository;

public class TrainingPlanEventRepository(Supabase.Client supabase) : BaseRepository<TrainingPlanEventEntity>(supabase), ITrainingPlanEventRepository
{
    
}