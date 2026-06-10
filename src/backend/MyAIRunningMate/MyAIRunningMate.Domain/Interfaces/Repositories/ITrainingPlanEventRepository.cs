using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.TrainingPlan;

public interface ITrainingPlanEventRepository : IBaseRepository<TrainingPlanEventEntity> 
{
    Task<IEnumerable<TrainingPlanEventEntity>> GetEventsForUserInDateRangeAsync(Guid trainingPlanId, DateTime start, DateTime end);
    Task<IEnumerable<TrainingPlanEventEntity>> GetEventsByPlanIdAsync(Guid trainingPlanId);
}