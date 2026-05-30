using MyAIRunningMate.Application.AggregatePage;
using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Client.Geocoder;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;

namespace MyAIRunningMate.Application.Insights;

public class InsightsService : IInsightsService
{
    private readonly IActivityViewService _activityViewService;
    private readonly IActivityRepository _activityRepository;
    private readonly IGeocodeClient _geocodingClient;
    
    public InsightsService(
        IActivityViewService activityViewService, IActivityRepository activityRepository, IGeocodeClient geocodingClient)
    {
        _activityViewService =  activityViewService;
        _activityRepository = activityRepository;
        _geocodingClient = geocodingClient;
    }
    
    public async Task<WeeklyInsights> GetWeeklyInsights(Guid userId)
    {
        var activityIdsThisWeek = await _activityRepository.GetCurrentWeekActivityIds(userId);

        var aggregateTasks = activityIdsThisWeek.Select(activityId => 
            _activityViewService.CreateAggregateActivity(activityId, userId)
        );
        var aggregateResults = await Task.WhenAll(aggregateTasks);
        var validActivities = aggregateResults.Where(a => true).ToList();

        if (validActivities.Count == 0)
        {
            return new WeeklyInsights
            {
                Locations = [],
                RestDays = 7
            };
        }
        
        var locationTasks = validActivities.Select(async a =>
        {
            if (!string.IsNullOrEmpty(a.Map?.MapPolyline))
            {
                var coordinates = PolylineDecoder.GetFirstCoordinate(a.Map.MapPolyline);
                if (coordinates.HasValue)
                {
                    return await _geocodingClient.GetReadableLocationAsync(coordinates.Value.Latitude, coordinates.Value.Longitude);
                }
            }

            return "Unknown Location";
        });
        
        var runningActivities = validActivities
            .Where(a => a.ExerciseType?.Equals("running", StringComparison.OrdinalIgnoreCase) ?? false)
            .ToList();
            
        var swimmingActivities = validActivities
            .Where(a => a.ExerciseType?.Equals("swimming", StringComparison.OrdinalIgnoreCase) ?? false)
            .ToList();
        
        var validHeartRates = validActivities
            .Where(a => a.AverageHeartRate > 0)
            .Select(a => a.AverageHeartRate)
            .ToList();

        var maxHeartRates = validActivities
            .Where(a => a.MaxHeartRate > 0)
            .Select(a => a.MaxHeartRate)
            .ToList();
        
        var uniqueActiveDaysCount = validActivities
            .Where(a => true)
            .Select(a => a.StartTime.Date)
            .Distinct()
            .Count();
        
        
        
        var resolvedLocations = (await Task.WhenAll(locationTasks)).ToList();

        if (swimmingActivities.Count != 0)
        {
            resolvedLocations.Add("Inspire Fitness Centre, Cabra, Dublin");
        }
            
        var restDays = Math.Max(0, 7 - uniqueActiveDaysCount);
        
        return new WeeklyInsights
        {
            RunningTimeVolume = runningActivities.Sum(a => a.DurationSeconds),
            RunningDistanceVolume = runningActivities.Sum(a => a.DistanceMetres ?? 0.0), 
            TotalRunningElevationGain = runningActivities.Sum(a => a.TotalElevationGain ?? 0),
            
            SwimmingTimeVolume = swimmingActivities.Sum(a => a.DurationSeconds),
            SwimmingDistanceVolume = swimmingActivities.Sum(a => a.DistanceMetres ?? 0.0),

            MeanAverageHeartRate = validHeartRates.Count != 0 ? (int)validHeartRates.Average()! : 0,
            MeanMaxHeartRate = maxHeartRates.Count != 0 ? (int)maxHeartRates.Max()! : 0,
            
            TotalTrainingEffect = validActivities.Sum(a => a.TrainingEffect ?? 0),
            MeanTrainingEffect = validActivities.Count != 0 ? validActivities.Average(a => a.TrainingEffect ?? 0) : 0,
            
            TotalAchievementCount = validActivities.Sum(a => a.AchievementCount ?? 0),
            TotalPersonalRecordCount = validActivities.Sum(a => a.PersonalRecordCount ?? 0),
            
            TotalPersonalExercises = validActivities.Count(a => !IsGroupSession(a)),
            TotalGroupExercises = validActivities.Count(IsGroupSession),
            
            Locations = resolvedLocations
                .Where(loc => loc != "Unknown Location")
                .Distinct(),

            RestDays = restDays
        };
        
        bool IsGroupSession(AggregateArtifactView artifact)
        {
            return artifact.AthleteCount > 1;
        }
    }
}