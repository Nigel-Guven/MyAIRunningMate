using MyAIRunningMate.Application.Models.ViewObjects;

namespace MyAIRunningMate.Application.TrainingPlans;

public interface ITrainingPlanService
{
    Task<TrainingPlanView> GenerateTrainingPlan(
        Guid userId, 
        string primaryGoal, 
        string runningYears, 
        string runningLevel, 
        int trainingPlanLength, 
        string poolSize);
    Task<TrainingPlanView> FinalizeTrainingPlan();
}