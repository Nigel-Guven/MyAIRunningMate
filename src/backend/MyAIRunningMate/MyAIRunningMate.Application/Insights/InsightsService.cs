using System.Globalization;
using MyAIRunningMate.Application.AggregatePage;
using MyAIRunningMate.Client.Geocoder;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Insights;

public class InsightsService(
    IActivityViewService activityViewService,
    IActivityRepository activityRepository,
    IGeocodeClient geocodingClient)
    : IInsightsService
{
    public async Task<WeeklyInsights> GetWeeklyInsights(Guid userId)
    {
        var activityIdsThisWeek = await activityRepository.GetCurrentWeekActivityIds(userId);

        var aggregateTasks = activityIdsThisWeek.Select(activityId => 
            activityViewService.CreateAggregateActivity(activityId, userId)
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
                    return await geocodingClient.GetReadableLocationAsync(coordinates.Value.Latitude, coordinates.Value.Longitude);
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

    public async Task<(YearlyStatistics Summary, IEnumerable<WeeklyInsights> WeeklyVolumes)> GetAnalyticsStatistics(Guid userId, int year)
    {
        var yearDate = new DateTime(year, 1, 1);

        var activitiesThisYear = (await activityRepository.GetAllActivitiesByYear(yearDate, userId))?.ToList();
        
        if (activitiesThisYear == null || activitiesThisYear.Count == 0)
        {
            return (new YearlyStatistics(), []);
        }

        var yearlyRunning = activitiesThisYear.Where(a => string.Equals(a.ExerciseType, "running", StringComparison.OrdinalIgnoreCase));
        var yearlySwimming = activitiesThisYear.Where(a => string.Equals(a.ExerciseType, "swimming", StringComparison.OrdinalIgnoreCase));

        var yearlyActiveDays = activitiesThisYear.Select(a => a.StartTime.Date).Distinct().Count();
        var yearlyTotalTrainingEffect = activitiesThisYear.Sum(a => a.TrainingEffect ?? 0);

        var yearlySummary = new YearlyStatistics
        {
            YearlyRunningDistance = (int)yearlyRunning.Sum(a => a.DistanceMetres ?? 0),
            YearlySwimmingDistance = (int)yearlySwimming.Sum(a => a.DistanceMetres ?? 0),
            YearlyActiveDays = yearlyActiveDays,
            YearlyTotalTrainingEffect = yearlyTotalTrainingEffect,
            YearlyAverageTrainingEffect = yearlyActiveDays > 0 ? yearlyTotalTrainingEffect / yearlyActiveDays : 0
        };
        
        var dfi = DateTimeFormatInfo.CurrentInfo;
        var cal = dfi.Calendar;

        var weeklyVolumes = activitiesThisYear
            .GroupBy(a => cal.GetWeekOfYear(a.StartTime, dfi.CalendarWeekRule, dfi.FirstDayOfWeek))
            .OrderBy(g => g.Key)
            .Select(weekGroup =>
            {
                var validActivities = weekGroup.ToList();

                var runningActivities = validActivities.Where(a => string.Equals(a.ExerciseType, "running", StringComparison.OrdinalIgnoreCase)).ToList();
                var swimmingActivities = validActivities.Where(a => string.Equals(a.ExerciseType, "swimming", StringComparison.OrdinalIgnoreCase)).ToList();
                
                var validHeartRates = validActivities.Where(a => a.AverageHeartRate > 0).Select(a => (double)a.AverageHeartRate).ToList();
                var maxHeartRates = validActivities.Where(a => a.MaxHeartRate > 0).Select(a => a.MaxHeartRate).ToList();
                
                var uniqueActiveDaysCount = validActivities.Select(a => a.StartTime.Date).Distinct().Count();

                //TODO: Store map strings in map database or separate address table
                //var resolvedLocations = new List<string>();
                //if (swimmingActivities.Count != 0)
                //{
                //    resolvedLocations.Add("Inspire Fitness Centre, Cabra, Dublin");
                //}

                return new WeeklyInsights
                {
                    RunningTimeVolume = runningActivities.Sum(a => a.DurationSeconds),
                    RunningDistanceVolume = runningActivities.Sum(a => a.DistanceMetres ?? 0.0), 
                    TotalRunningElevationGain = runningActivities.Sum(a => a.TotalElevationGain ?? 0),
                    
                    SwimmingTimeVolume = swimmingActivities.Sum(a => a.DurationSeconds),
                    SwimmingDistanceVolume = swimmingActivities.Sum(a => a.DistanceMetres ?? 0.0),

                    MeanAverageHeartRate = validHeartRates.Count != 0 ? (int)validHeartRates.Average() : 0,
                    MeanMaxHeartRate = maxHeartRates.Count != 0 ? maxHeartRates.Max() : 0,
                    
                    TotalTrainingEffect = validActivities.Sum(a => a.TrainingEffect ?? 0),
                    MeanTrainingEffect = validActivities.Count != 0 ? validActivities.Average(a => a.TrainingEffect ?? 0) : 0,
                    
                    // Unused
                    TotalAchievementCount = 0,
                    TotalPersonalRecordCount = 0,
                    TotalPersonalExercises = 0,
                    TotalGroupExercises = 0,
                    
                    Locations = [],
                    RestDays = Math.Max(0, 7 - uniqueActiveDaysCount)
                };
            })
            .ToList();
        
            return (yearlySummary, weeklyVolumes);
        }
}