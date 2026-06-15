using MyAIRunningMate.Application.Weights;
using MyAIRunningMate.Client.Python;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.TrainingPlans;

public class TrainingPlanService(
    IActivityRepository activityRepository,
    IWeightService weightService,
    IPythonApiClient pythonApiClient,
    ITrainingPlanRepository trainingPlanRepository,
    ITrainingPlanEventRepository trainingPlanEventRepository)
    : ITrainingPlanService
{
    public async Task<TrainingPlanView> GenerateTrainingPlanAsync(Guid userId, string primaryGoal, string runningExperience, string runningLevel, int trainingPlanLength, string poolSize)
    {
        var currentWeight = await weightService.GetLatestWeightAsync(userId);
        var weightPounds = currentWeight?.WeightInPounds ?? 150;
        
        var lastTenActivities = await activityRepository.GetLatestActivities(userId);
        
        var mappedRunningExperience = MapExperienceYears(runningExperience);
        var mappedRunningLevel = MapRunningLevel(runningLevel);

        var response = await pythonApiClient.GenerateTrainingPlanAsync(
            primaryGoal,
            mappedRunningExperience,
            mappedRunningLevel,
            trainingPlanLength,
            poolSize,
            weightPounds,
            lastTenActivities,
            userId);
        
        return new TrainingPlanView(response.TrainingPlan, response.Events);
    }

    public async Task<TrainingPlanFinalizeResult> FinalizeTrainingPlanAsync(Guid userId, TrainingPlanView plan)
    {
        await trainingPlanRepository.InsertAsync(plan.GeneratedTrainingPlan);
        
        await trainingPlanEventRepository.BulkInsertAsync(plan.TrainingPlanEvents);

        return new TrainingPlanFinalizeResult(
            plan.GeneratedTrainingPlan.TrainingPlanId,
            $"Training plan \"{plan.GeneratedTrainingPlan.Title}\" saved at time {plan.GeneratedTrainingPlan.CreatedAt}.",
            plan.TrainingPlanEvents.Count()
        );
    }

    public async Task<TrainingPlanView?> GetTrainingPlanByIdAsync(Guid trainingPlanId)
    {
        var trainingPlan = await trainingPlanRepository.GetByIdAsync(trainingPlanId);
        
        if (trainingPlan == null) 
            return null;
        
        var eventEntities = await trainingPlanEventRepository.GetEventsByPlanIdAsync(trainingPlanId);
        
        return new TrainingPlanView(trainingPlan, eventEntities);
    }

    public async Task<TrainingPlanView?> GetActivePlanForUserAsync(Guid userId, DateTime startOfMonth, DateTime endOfMonth)
    {
        var allUserPlans = await trainingPlanRepository.GetAllPlansForUserAsync(userId);
        if (!allUserPlans.Any()) return null;

        var activePlanEntity = allUserPlans.FirstOrDefault(plan => 
            plan.StartDate <= endOfMonth && plan.EndDate >= startOfMonth
        );

        if (activePlanEntity == null) return null;

        var eventEntities = await trainingPlanEventRepository.GetEventsByPlanIdAsync(activePlanEntity.TrainingPlanId);

        return new TrainingPlanView(activePlanEntity, eventEntities);
    }

    private static int MapExperienceYears(string experienceYears) =>
        experienceYears switch
        {
            "1 or Less" => 1,
            "2-3" => 2,
            "4+ years" => 4,
            _ => 5,
        };

    private static string MapRunningLevel(string runningLevel) =>
        runningLevel.Equals("Expert", StringComparison.OrdinalIgnoreCase)
            ? "Advanced"
            : runningLevel;
}