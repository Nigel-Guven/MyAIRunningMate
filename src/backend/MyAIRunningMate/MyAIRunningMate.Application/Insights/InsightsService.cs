using System.Globalization;
using MyAIRunningMate.Application.AggregatePage;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Insights;

public class InsightsService(
    IActivityViewService activityViewService,
    IActivityRepository activityRepository)
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
        
        var runningActivities = validActivities
            .Where(a => a.GarminActivity.ExerciseType?.Equals("running", StringComparison.OrdinalIgnoreCase) ?? false)
            .ToList();
            
        var swimmingActivities = validActivities
            .Where(a => a.GarminActivity.ExerciseType?.Equals("swimming", StringComparison.OrdinalIgnoreCase) ?? false)
            .ToList();
        
        var validHeartRates = validActivities
            .Where(a => a.GarminActivity.AverageHeartRate > 0)
            .Select(a => a.GarminActivity.AverageHeartRate)
            .ToList();

        var maxHeartRates = validActivities
            .Where(a => a.GarminActivity.MaxHeartRate > 0)
            .Select(a => a.GarminActivity.MaxHeartRate)
            .ToList();
        
        var uniqueActiveDaysCount = validActivities
            .Where(a => true)
            .Select(a => a.GarminActivity.StartTime.Date)
            .Distinct()
            .Count();

        var restDays = Math.Max(0, 7 - uniqueActiveDaysCount);
        
        return new WeeklyInsights
        {
            RunningTimeVolume = runningActivities.Sum(a => a.GarminActivity.DurationSeconds),
            RunningDistanceVolume = runningActivities.Sum(a => a.GarminActivity.DistanceMetres), 
            TotalRunningElevationGain = runningActivities.Sum(a => a.GarminActivity.TotalElevationGain ?? 0),
            
            SwimmingTimeVolume = swimmingActivities.Sum(a => a.GarminActivity.DurationSeconds),
            SwimmingDistanceVolume = swimmingActivities.Sum(a => a.GarminActivity.DistanceMetres),

            MeanAverageHeartRate = validHeartRates.Count != 0 ? (int)validHeartRates.Average()! : 0,
            MeanMaxHeartRate = maxHeartRates.Count != 0 ? (int)maxHeartRates.Max()! : 0,
            
            TotalTrainingEffect = validActivities.Sum(a => a.GarminActivity.TrainingEffect),
            MeanTrainingEffect = validActivities.Count != 0 ? validActivities.Average(a => a.GarminActivity.TrainingEffect) : 0,

            RestDays = restDays
        };
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
        var yearlyTotalTrainingEffect = activitiesThisYear.Sum(a => a.TrainingEffect);

        var yearlySummary = new YearlyStatistics
        {
            YearlyRunningDistance = (int)yearlyRunning.Sum(a => a.DistanceMetres),
            YearlySwimmingDistance = (int)yearlySwimming.Sum(a => a.DistanceMetres),
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

                return new WeeklyInsights
                {
                    RunningTimeVolume = runningActivities.Sum(a => a.DurationSeconds),
                    RunningDistanceVolume = runningActivities.Sum(a => a.DistanceMetres), 
                    TotalRunningElevationGain = runningActivities.Sum(a => a.TotalElevationGain ?? 0),
                    
                    SwimmingTimeVolume = swimmingActivities.Sum(a => a.DurationSeconds),
                    SwimmingDistanceVolume = swimmingActivities.Sum(a => a.DistanceMetres ),

                    MeanAverageHeartRate = validHeartRates.Count != 0 ? (int)validHeartRates.Average() : 0,
                    MeanMaxHeartRate = maxHeartRates.Count != 0 ? maxHeartRates.Max() : 0,
                    
                    TotalTrainingEffect = validActivities.Sum(a => a.TrainingEffect),
                    MeanTrainingEffect = validActivities.Count != 0 ? validActivities.Average(a => a.TrainingEffect) : 0,
                    
                    Locations = [],
                    RestDays = Math.Max(0, 7 - uniqueActiveDaysCount)
                };
            })
            .ToList();
        
            return (yearlySummary, weeklyVolumes);
        }
}