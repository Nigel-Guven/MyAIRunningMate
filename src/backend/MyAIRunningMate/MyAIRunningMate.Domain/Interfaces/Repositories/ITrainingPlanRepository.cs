using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface ITrainingPlanRepository
{
    Task<TrainingPlan?> GetByIdAsync(Guid trainingPlanId);
    Task<TrainingPlan?> GetActivePlanForUserAsync(Guid userId, DateTime startOfMonth, DateTime endOfMonth);
    Task<IEnumerable<TrainingPlan>> GetAllPlansForUserAsync(Guid userId);
    Task InsertAsync(TrainingPlan trainingPlan);
}