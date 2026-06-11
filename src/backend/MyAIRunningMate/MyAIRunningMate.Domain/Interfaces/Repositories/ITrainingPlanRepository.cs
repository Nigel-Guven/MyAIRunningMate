namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface ITrainingPlanRepository
{
    Task<Models.TrainingPlan?> GetByIdAsync(Guid trainingPlanId);
    Task<Models.TrainingPlan?> GetActivePlanForUserAsync(Guid userId, DateTime startOfMonth, DateTime endOfMonth);
    Task<IEnumerable<Models.TrainingPlan>> GetAllPlansForUserAsync(Guid userId);
}