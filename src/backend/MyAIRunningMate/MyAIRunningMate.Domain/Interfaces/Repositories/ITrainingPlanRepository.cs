using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.TrainingPlan;

public interface ITrainingPlanRepository : IBaseRepository<TrainingPlanEntity> 
{
    Task<TrainingPlanEntity?> GetByIdAsync(Guid trainingPlanId);
    Task<TrainingPlanEntity?> GetActivePlanForUserAsync(Guid userId, DateTime startOfMonth, DateTime endOfMonth);
    Task<IEnumerable<TrainingPlanEntity>> GetAllPlansForUserAsync(Guid userId);
}