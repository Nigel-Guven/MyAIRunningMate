using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Application.Weight;
using MyAIRunningMate.Client.Python;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;

namespace MyAIRunningMate.Application.TrainingPlans;

public class TrainingPlanService : ITrainingPlanService
{
    private readonly IActivityRepository _activityRepository;
    private readonly IWeightService _weightService;
    private readonly IPythonApiClient _pythonApiClient;

    public TrainingPlanService(
        IActivityRepository activityRepository,
        IWeightService weightService,
        IPythonApiClient pythonApiClient)
    {
        _activityRepository = activityRepository;
        _weightService = weightService;
        _pythonApiClient = pythonApiClient;
    }
    
    public async Task<TrainingPlanView> GenerateTrainingPlan(Guid userId, string primaryGoal, int runningExperience, string runningLevel, int trainingPlanLength, string poolSize)
    {
        var currentWeight = await _weightService.GetLatestWeightAsync(userId);
        var lastTenActivities = await _activityRepository.GetLatestActivities(userId);

        var activities = lastTenActivities.Select(entity => entity.ToActivityView());

        _pythonApiClient.ProcessTrainingPlanRequisites(primaryGoal, runningExperience, runningLevel, trainingPlanLength, poolSize, currentWeight.WeightPounds, activities);
    }

    public Task<TrainingPlanView> FinalizeTrainingPlan()
    {
        throw new NotImplementedException();
    }
}