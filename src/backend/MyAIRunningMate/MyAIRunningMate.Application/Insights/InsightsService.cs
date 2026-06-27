using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Insights;

public class InsightsService(
    IActivityRepository activityRepository)
    : IInsightsService
{
    public async Task<WeeklyInsights> GetWeeklyInsights(Guid userId, int weekOffset)
    {
        var weekDates = GetFirstAndLastDatesOfWeek(weekOffset); 
        
        var activityIdsThisWeek = await activityRepository.GetCurrentWeekActivityIds(userId, weekDates.Item1, weekDates.Item2);

        var aggregateTasks = activityIdsThisWeek.Select(activityId => 
            activityRepository.GetActivityByActivityId(activityId, userId)
        );
        var activities = await Task.WhenAll(aggregateTasks);
        
        var validActivities = activities
            .Where(a => a != null)
            .OfType<Activity>() 
            .ToList();

        return validActivities.Count switch
        {
            0 => new WeeklyInsights
            {
                Locations = [],
                RestDays = 7
            },
            _ => CalculateWeeklyMetrics(validActivities)
        };
    }

    public Task<(YearlyStatistics Summary, IEnumerable<WeeklyInsights> WeeklyVolumes)> GetAnalyticsStatistics(Guid userId, int year) => throw new NotImplementedException();

    /*
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
        }*/
    
    private static WeeklyInsights CalculateWeeklyMetrics(List<Activity> activities)
    {
        int morningActivityCount = 0, afternoonActivityCount = 0, eveningActivityCount =0, nightActivityCount = 0;
        double entropy = 0;
        foreach (var hour in activities.Select(a => a.StartTime.Hour))
        {
            switch (hour)
            {
                case >= 6 and < 12:
                    morningActivityCount++;
                    break;
                case >= 12 and < 17:
                    afternoonActivityCount++;
                    break;
                case >= 17 and < 21:
                    eveningActivityCount++;
                    break;
                default:
                    nightActivityCount++;
                    break;
            }
        }
        
        var activitiesPerDay = activities
            .GroupBy(a => a.StartTime.Date)
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
        
        double timeOfDayScore = entropy / Math.Log(4);
        
        var runningActivities = activities
            .Where(a => string.Equals(a.ExerciseType, "running", StringComparison.OrdinalIgnoreCase))
            .ToList();
        
        var swimmingActivities = activities
            .Where(a => string.Equals(a.ExerciseType, "swimming", StringComparison.OrdinalIgnoreCase))
            .ToList();
        
        var validHeartRates = activities.Where(a => a.AverageHeartRate > 0).Select(a => (double)a.AverageHeartRate).ToList();
        var maxHeartRates = activities.Where(a => a.MaxHeartRate > 0).Select(a => a.MaxHeartRate).ToList();

        var locations = activities
            .Where(a => !string.IsNullOrEmpty(a.Location))
            .Select(a => a.Location!)
            .Distinct()
            .ToList();
        
        var uniqueActiveDaysCount = activities.Select(a => a.StartTime.Date).Distinct().Count();
        
        var dayConsistencyScore = activities.Count == 0 ? 0 : Math.Min(1.0, uniqueActiveDaysCount / 7.0);
        
        return new WeeklyInsights
        {
            RunningTimeSeconds = runningActivities.Sum(a => a.DurationSeconds),
            RunningMovingTimeSeconds = runningActivities.Sum(a => a.MovingTimeSeconds),
            RunningDistanceMetres = runningActivities.Sum(a => a.DistanceMetres),
            TotalRunningElevationGain = runningActivities.Sum(a => a.TotalElevationGain ?? 0),
            
            SwimmingTimeSeconds = swimmingActivities.Sum(a => a.DurationSeconds),
            SwimmingDistanceMetres = swimmingActivities.Sum(a => a.DistanceMetres),
            
            TotalCaloriesBurned = activities.Sum(a => a.Calories),

            MeanAverageHeartRate = validHeartRates.Count != 0 ? (int)validHeartRates.Average() : 0,
            MeanMaxHeartRate = maxHeartRates.Count != 0 ? maxHeartRates.Max() : 0,
            
            TotalTrainingEffect = activities.Sum(a => a.TrainingEffect),
            MeanTrainingEffect = activities.Count != 0 ? activities.Average(a => a.TrainingEffect) : 0,
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
}