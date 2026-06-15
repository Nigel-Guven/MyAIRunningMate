using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.TrainingPlans;

public interface ITrainingPlanService
{
    Task<TrainingPlanView> GenerateTrainingPlanAsync(
        Guid userId, 
        string primaryGoal, 
        string runningYears, 
        string runningLevel, 
        int trainingPlanLength, 
        string poolSize);
    
    Task<TrainingPlanFinalizeResult> FinalizeTrainingPlanAsync(Guid userId, TrainingPlanView plan);
    
    Task<TrainingPlanView?> GetTrainingPlanByIdAsync(Guid trainingPlanId);
    Task<TrainingPlanView?> GetActivePlanForUserAsync(Guid userId, DateTime startOfMonth, DateTime endOfMonth);
}