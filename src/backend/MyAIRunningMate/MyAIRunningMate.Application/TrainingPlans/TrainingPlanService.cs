using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Application.Weights;
using MyAIRunningMate.Client.Python;
using MyAIRunningMate.Client.Python.Requests;
using MyAIRunningMate.Client.Python.Responses;
using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;
using MyAIRunningMate.Domain.Interfaces.Repositories.TrainingPlan;

namespace MyAIRunningMate.Application.TrainingPlans;

public class TrainingPlanService : ITrainingPlanService
{
    private readonly IActivityRepository _activityRepository;
    private readonly IWeightService _weightService;
    private readonly IPythonApiClient _pythonApiClient;
    private readonly ITrainingPlanRepository _trainingPlanRepository;
    private readonly ITrainingPlanEventRepository _trainingPlanEventRepository;

    public TrainingPlanService(
        IActivityRepository activityRepository,
        IWeightService weightService,
        IPythonApiClient pythonApiClient,
        ITrainingPlanRepository trainingPlanRepository,
        ITrainingPlanEventRepository trainingPlanEventRepository)
    {
        _activityRepository = activityRepository;
        _weightService = weightService;
        _pythonApiClient = pythonApiClient;
        _trainingPlanRepository = trainingPlanRepository;
        _trainingPlanEventRepository = trainingPlanEventRepository;
    }
    
    public async Task<TrainingPlanView> GenerateTrainingPlan(Guid userId, string primaryGoal, string runningExperience, string runningLevel, int trainingPlanLength, string poolSize)
    {
        
        
        var currentWeight = await _weightService.GetLatestWeightAsync(userId);
        var weightPounds = currentWeight?.WeightPounds ?? 150;
        var lastTenActivities = await _activityRepository.GetLatestActivities(userId);

        var activities = lastTenActivities.Select(entity => entity.ToActivityView());

        var activityRequest = activities.Select(x => new PythonApiActivity
        {
            ExerciseType = x.ExerciseType,
            AverageHeartRate = x.AverageHeartRate,
            AverageSecondPerKilometre = x.AverageSecondPerKilometre ?? 0,
            DistanceMetres = x.DistanceMetres ?? 0,
            DurationSeconds = x.DurationSeconds,
            MaxHeartRate = x.MaxHeartRate,
            StartTime = x.StartTime,
            TotalElevationGain = x.TotalElevationGain ?? 0,
            TrainingEffect = x.TrainingEffect ?? 0,
        }).ToList();

        var mappedRunningExperience = MapExperienceYears(runningExperience);
        var mappedRunningLevel = MapRunningLevel(runningLevel);

        var response = await _pythonApiClient.ProcessTrainingPlanRequisites(
            primaryGoal,
            mappedRunningExperience,
            mappedRunningLevel,
            trainingPlanLength,
            poolSize,
            weightPounds,
            activityRequest);
        
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

    public async Task<TrainingPlanFinalizeResult> FinalizeTrainingPlanAsync(Guid userId, TrainingPlanView plan)
    {
        if (string.IsNullOrWhiteSpace(plan.Title))
        {
            throw new ArgumentException("Training plan title is required.");
        }

        if (plan.TrainingPlanEvents == null || !plan.TrainingPlanEvents.Any())
        {
            throw new ArgumentException("Training plan must include at least one event.");
        }

        foreach (var trainingEvent in plan.TrainingPlanEvents)
        {
            if (string.IsNullOrWhiteSpace(trainingEvent.ExerciseType))
            {
                throw new ArgumentException("Each event must have an exercise type.");
            }

            if (string.IsNullOrWhiteSpace(trainingEvent.ExerciseSubtype))
            {
                throw new ArgumentException("Each event must have an exercise subtype.");
            }

            if (trainingEvent.DistanceMetres < 0)
            {
                throw new ArgumentException("Distance cannot be negative.");
            }
        }

        var planEntity = new TrainingPlanEntity
        {
            Title = plan.Title.Trim(),
            Description = string.IsNullOrWhiteSpace(plan.Description) ? string.Empty : plan.Description.Trim(),
            StartDate = plan.StartDate,
            EndDate = plan.EndDate,
            UserId = userId,
        };

        var savedPlan = await _trainingPlanRepository.Insert(planEntity);

        var eventEntities = plan.TrainingPlanEvents.Select(trainingEvent => new TrainingPlanEventEntity
        {
            TrainingPlanId = savedPlan.TrainingPlanId,
            EventDate = trainingEvent.EventDate,
            ExerciseType = trainingEvent.ExerciseType.Trim(),
            ExerciseSubtype = trainingEvent.ExerciseSubtype.Trim(),
            Description = string.IsNullOrWhiteSpace(trainingEvent.Description)
                ? string.Empty
                : trainingEvent.Description.Trim(),
            DistanceMetres = trainingEvent.DistanceMetres,
        }).ToList();

        var savedEvents = await _trainingPlanEventRepository.BulkInsert(eventEntities);

        return new TrainingPlanFinalizeResult
        {
            TrainingPlanId = savedPlan.TrainingPlanId,
            Message = $"Training plan \"{plan.Title}\" saved with {savedEvents.Count()} sessions.",
            EventsSaved = savedEvents.Count(),
        };
    }

    public async Task<TrainingPlanView?> GetTrainingPlanByIdAsync(Guid trainingPlanId)
    {
        var trainingPlanEntity = await _trainingPlanRepository.GetById(trainingPlanId);
        
        if (trainingPlanEntity == null) 
            return null;
        
        var eventEntities = await _trainingPlanEventRepository.GetEventsByPlanIdAsync(trainingPlanId);
        
        return new TrainingPlanView
        {
            Title = trainingPlanEntity.Title,
            Description = trainingPlanEntity.Description,
            StartDate = trainingPlanEntity.StartDate,
            EndDate = trainingPlanEntity.EndDate,
            TrainingPlanEvents = eventEntities.Select(ev => new TrainingPlanEventView
            {
                EventDate = ev.EventDate,
                ExerciseType = ev.ExerciseType,
                ExerciseSubtype = ev.ExerciseSubtype,
                Description = ev.Description,
                DistanceMetres = ev.DistanceMetres
            }).ToList()
        };
    }

    public async Task<TrainingPlanView?> GetActivePlanForUserAsync(Guid userId, DateTime startOfMonth, DateTime endOfMonth)
    {
        var allUserPlans = await _trainingPlanRepository.GetAllPlansForUserAsync(userId);
        if (allUserPlans == null || !allUserPlans.Any()) return null;

        var activePlanEntity = allUserPlans.FirstOrDefault(plan => 
            plan.StartDate <= endOfMonth && plan.EndDate >= startOfMonth
        );

        if (activePlanEntity == null) return null;

        var eventEntities = await _trainingPlanEventRepository.GetEventsByPlanIdAsync(activePlanEntity.TrainingPlanId);
        if (eventEntities == null) return null;

        return new TrainingPlanView
        {
            Title = activePlanEntity.Title,
            Description = activePlanEntity.Description,
            StartDate = activePlanEntity.StartDate,
            EndDate = activePlanEntity.EndDate,
            TrainingPlanEvents = eventEntities.Select(ev => new TrainingPlanEventView
            {
                EventDate = ev.EventDate,
                ExerciseType = ev.ExerciseType,
                ExerciseSubtype = ev.ExerciseSubtype,
                Description = ev.Description,
                DistanceMetres = ev.DistanceMetres
            }).ToList()
        };
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