using MyAIRunningMate.Domain.Models.ViewObjects;

namespace MyAIRunningMate.Application.TrainingPlans;

public interface ITrainingPlanService
{
    Task<TrainingPlan> GenerateTrainingPlan(
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