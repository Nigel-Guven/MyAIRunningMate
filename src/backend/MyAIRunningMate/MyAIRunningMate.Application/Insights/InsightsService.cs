using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Insights;

public class InsightsService(
    IActivityRepository activityRepository, IActivityMetricsRepository activityMetricsRepository)
    : IInsightsService
{
    public async Task<WeeklyInsights> GetWeeklyInsights(Guid userId, int weekOffset)
    {
        var weekDates = GetFirstAndLastDatesOfWeek(weekOffset); 
        
        var activityIdsThisWeek = await activityRepository.GetCurrentWeekActivityIds(userId, weekDates.Item1, weekDates.Item2);

        if (!activityIdsThisWeek.Any())
        {
            return GetEmptyWeeklyInsights();
        }
        
        var fetchTasks = activityIdsThisWeek.Select(async activityId =>
        {
            var activityTask = activityRepository.GetActivityByActivityId(activityId, userId);
            var metricsTask = activityMetricsRepository.GetActivityMetrics(activityId);

            await Task.WhenAll(activityTask, metricsTask);

            return new InsightsActivity
            {
                Activity = await activityTask,
                ActivityMetrics = await metricsTask
            };
        });
        
        var insightsActivities = await Task.WhenAll(fetchTasks);
        
        var validInsights = insightsActivities
            .Where(ia => true)
            .ToList();

        return validInsights.Count == 0 
            ? GetEmptyWeeklyInsights() 
            : CalculateWeeklyMetrics(validInsights);
    }

    public Task<(YearlyStatistics Summary, IEnumerable<WeeklyInsights> WeeklyVolumes)> GetAnalyticsStatistics(Guid userId, int year) => throw new NotImplementedException();

    private static WeeklyInsights CalculateWeeklyMetrics(List<InsightsActivity> activities)
    {
        int morningActivityCount = 0, afternoonActivityCount = 0, eveningActivityCount =0, nightActivityCount = 0;
        double entropy = 0;
        foreach (var hour in activities.Select(a => a.Activity.StartTime.Hour))
        {
            switch (hour)
            {
                case >= 6 and < 12:
                    morningActivityCount++;
                    break;
                case >= 12 and < 17:
                    afternoonActivityCount++;
                    break;
                case >= 16 and < 20:
                    eveningActivityCount++;
                    break;
                default:
                    nightActivityCount++;
                    break;
            }
        }
        
        var activitiesPerDay = activities
            .GroupBy(a => a.Activity.StartTime.Date)
            .Select(g => g.Count())
            .ToList();
        
        var averageActivitiesPerDay = activitiesPerDay.Average();
        var variance = activitiesPerDay.Average(x => Math.Pow(x - averageActivitiesPerDay, 2));
        var standardDeviationActivitiesPerDay = Math.Sqrt(variance);
        
        var coefficientOfVariation = averageActivitiesPerDay == 0 ? 0 : standardDeviationActivitiesPerDay / averageActivitiesPerDay;

        var distributionScore = 1.0 / (1.0 + coefficientOfVariation);
        
        double total = morningActivityCount + afternoonActivityCount + eveningActivityCount + nightActivityCount;
        
        if (total > 0)
        {
            double[] probs =
            [
                morningActivityCount / total,
                afternoonActivityCount / total,
                eveningActivityCount / total,
                nightActivityCount / total
            ];

            entropy = -probs.Where(p => p > 0)
                .Sum(p => p * Math.Log(p));
        }
        
        var timeOfDayScore = entropy / Math.Log(4);
        
        var runningActivities = activities
            .Where(a => string.Equals(a.Activity.ExerciseType, "running", StringComparison.OrdinalIgnoreCase))
            .ToList();
        
        var swimmingActivities = activities
            .Where(a => string.Equals(a.Activity.ExerciseType, "swimming", StringComparison.OrdinalIgnoreCase))
            .ToList();
        
        var validHeartRates = activities.Where(a => a.ActivityMetrics.AverageHeartRate > 0).Select(a => (double)a.ActivityMetrics.AverageHeartRate).ToList();
        var maxHeartRates = activities.Where(a => a.ActivityMetrics.MaxHeartRate > 0).Select(a => a.ActivityMetrics.MaxHeartRate).ToList();

        var locations = activities
            .Where(a => !string.IsNullOrEmpty(a.Activity.Location))
            .Select(a => a.Activity.Location!)
            .Distinct()
            .ToList();
        
        var uniqueActiveDaysCount = activities.Select(a => a.Activity.StartTime.Date).Distinct().Count();
        
        var dayConsistencyScore = activities.Count == 0 ? 0 : Math.Min(1.0, uniqueActiveDaysCount / 7.0);
        
        return new WeeklyInsights
        {
            RunningTimeSeconds = runningActivities.Sum(a => a.Activity.TotalTime),
            RunningMovingTimeSeconds = runningActivities.Sum(a => a.Activity.MovingTime),
            RunningDistanceMetres = runningActivities.Sum(a => a.Activity.DistanceMetres),
            TotalRunningElevationGain = runningActivities.Sum(a => a.Activity.TotalAscent ?? 0), //TODO
            
            SwimmingTimeSeconds = swimmingActivities.Sum(a => a.Activity.TotalTime),
            SwimmingDistanceMetres = swimmingActivities.Sum(a => a.Activity.DistanceMetres),
            
            TotalCaloriesBurned = activities.Sum(a => a.ActivityMetrics.TotalCalories),

            MeanAverageHeartRate = validHeartRates.Count != 0 ? (int)validHeartRates.Average() : 0,
            MeanMaxHeartRate = maxHeartRates.Count != 0 ? maxHeartRates.Max() : 0,
            
            TotalTrainingEffect = activities.Sum(a => a.ActivityMetrics.AerobicTrainingEffect),
            MeanTrainingEffect = activities.Count != 0 ? activities.Average(a => a.ActivityMetrics.AerobicTrainingEffect) : 0,
            TrainingConsistencyScore = Math.Round(
                (0.5 * dayConsistencyScore) + 
                (0.3 * distributionScore) + 
                (0.2 * timeOfDayScore),3),

            MorningActivities = morningActivityCount,
            AfternoonActivities = afternoonActivityCount,
            EveningActivities = eveningActivityCount,
            NightActivities = nightActivityCount,
        
            Locations = locations,
            RestDays = Math.Max(0, 7 - uniqueActiveDaysCount)
        };
    }

    private static Tuple<DateTime, DateTime> GetFirstAndLastDatesOfWeek(int weekOffset)
    {
        var today = DateTime.UtcNow.Date;
        
        var diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
        var currentWeekStart = today.Date.AddDays(-diff);
        
        var targetWeekStart = currentWeekStart.AddDays(weekOffset * 7);
        var targetWeekEnd = targetWeekStart.AddDays(7);
        
        return  Tuple.Create(targetWeekStart, targetWeekEnd);
    }
    
    private static WeeklyInsights GetEmptyWeeklyInsights() => new()
    {
        Locations = [],
        RestDays = 7
    };
}