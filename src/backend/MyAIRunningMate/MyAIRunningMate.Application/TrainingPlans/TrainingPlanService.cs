using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Application.Weight;
using MyAIRunningMate.Client.Python;
using MyAIRunningMate.Client.Python.Requests;
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
    
    public async Task<TrainingPlanView> GenerateTrainingPlan(Guid userId, string primaryGoal, string runningExperience, string runningLevel, int trainingPlanLength, string poolSize)
    {
        
        
        var currentWeight = await _weightService.GetLatestWeightAsync(userId);
        var weightPounds = currentWeight?.WeightPounds ?? 150;
        var lastTenActivities = await _activityRepository.GetLatestActivities(userId);

        var activities = lastTenActivities.Select(entity => entity.ToActivityView());

        var activityRequest = activities.Select(x => new PythonApiActivity()
        {
            ExerciseType = x.ExerciseType,
            AverageHeartRate = x.AverageHeartRate,
            AverageSecondPerKilometre =  x.AverageSecondPerKilometre,
            DistanceMetres =  x.DistanceMetres,
            DurationSeconds =  x.DurationSeconds,
            MaxHeartRate = x.MaxHeartRate,
            StartTime =  x.StartTime,
            TotalElevationGain =   x.TotalElevationGain,
            TrainingEffect =  x.TrainingEffect,
        });
        
        var mappedRunningExperience = MapExperienceYears(runningExperience);

        var response = await _pythonApiClient.ProcessTrainingPlanRequisites(primaryGoal, mappedRunningExperience, runningLevel, trainingPlanLength, poolSize, weightPounds, activityRequest);
        
        var trainingPlanView = new TrainingPlanView()
        {
            Title = response.Title,
            Description = response.Description,
            EndDate = response.EndDate,
            StartDate = response.StartDate,
            TrainingPlanEvents = response.TrainingPlanEvents
                .Select(ev => new TrainingPlanEventView
                {
                    EventDate = ev.EventDate,
                    ExerciseType = ev.ExerciseType,
                    ExerciseSubtype = ev.ExerciseSubtype,
                    Description = ev.Description,
                    DistanceMetres = ev.DistanceMetres,
                })
                .ToList()
        };
        
        return trainingPlanView;
    }

    public Task<TrainingPlanView> FinalizeTrainingPlan()
    {
        throw new NotImplementedException();
    }
    
    private static int MapExperienceYears(string experienceYears) =>
        experienceYears switch
        {
            "1 or Less" => 1,
            "2-3" => 2,
            "4+ years" => 4,
            _ => 5,
        };
}