using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Insights;

public class InsightsService(
    IActivityRepository activityRepository, IActivityMetricsRepository activityMetricsRepository, IWeightRepository weightRepository, IFitnessMetricsCalculator fitnessMetricsCalculator)
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

    public async Task<UserMetrics> GetUserMetrics(Guid userId)
    {
        var latestWeight = await weightRepository.GetLatestWeight(userId);
        var latestActivity = await activityRepository.GetLatestActivity(userId);

        var v02Max = latestActivity.UserVolumetricOxygenMax;
        var weightKg = Math.Round(latestWeight.WeightInPounds * 0.4539, 1);
        
        var powerToWeightRatio = fitnessMetricsCalculator.CalculatePowerToWeight(latestActivity.UserLactateThresholdPower, weightKg);
        
        return new UserMetrics
        {
            WeightKg = weightKg,
            UserVolumetricOxygenMax = v02Max,
            UserMaxHeartRate = latestActivity.UserMaxHeartRate,
            UserLactateThresholdHeartRate = latestActivity.UserLactateThresholdHeartRate,
            UserLactateThresholdPower = latestActivity.UserLactateThresholdPower,
            UserLactateThresholdSpeed = latestActivity.UserLactateThresholdSpeed,
            
            //DerivedMetrics
            UserVolumetricOxygenMaxRating = fitnessMetricsCalculator.GetVo2MaxRating(v02Max),
            FitnessPercentile = fitnessMetricsCalculator.GetFitnessPercentile(v02Max),
            PowerToWeightRatio = powerToWeightRatio,
            PowerRating = fitnessMetricsCalculator.GetPowerRating(powerToWeightRatio),
            ThresholdPercentagePower = fitnessMetricsCalculator.CalculateThresholdPercentageOfMaxHr(latestActivity.UserLactateThresholdHeartRate,  latestActivity.UserMaxHeartRate),
            TrainingLevel = fitnessMetricsCalculator.GetTrainingLevel(v02Max, powerToWeightRatio),
            FitnessRankColor = fitnessMetricsCalculator.GetFitnessRankColor(v02Max)
        };
    }

    public Task<(YearlyStatistics Summary, IEnumerable<WeeklyInsights> WeeklyVolumes)> GetAnalyticsStatistics(Guid userId, int year) => throw new NotImplementedException();

    private static WeeklyInsights CalculateWeeklyMetrics(List<InsightsActivity> activities)
    {
        int morningActivityCount = 0, afternoonActivityCount = 0, eveningActivityCount =0, nightActivityCount = 0;
        
        foreach (var hour in activities.Select(a => a.Activity.StartTime.Hour))
        {
            switch (hour)
            {
                case >= 6 and < 12:
                    morningActivityCount++;
                    break;
                case >= 12 and < 16:
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
        
        var runningActivities = activities
            .Where(a => string.Equals(a.Activity.ExerciseType, "running", StringComparison.OrdinalIgnoreCase))
            .ToList();
        
        var swimmingActivities = activities
            .Where(a => string.Equals(a.Activity.ExerciseType, "swimming", StringComparison.OrdinalIgnoreCase))
            .ToList();

        var otherActivities = activities
            .Where(a => !string.Equals(a.Activity.ExerciseType, "running", StringComparison.OrdinalIgnoreCase) && 
                        !string.Equals(a.Activity.ExerciseType, "swimming", StringComparison.OrdinalIgnoreCase))
            .ToList();
        
        var otherActivityTypes = otherActivities
            .Select(a => a.Activity.ExerciseName)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var otherDistanceMetres = otherActivities
            .Sum(a => a.Activity.DistanceMetres);
        var otherTotalTimeSeconds = otherActivities
            .Sum(a => a.Activity.TotalTime);
        
        var locations = activities
            .Where(a => !string.IsNullOrEmpty(a.Activity.Location))
            .Select(a => a.Activity.Location!)
            .Distinct()
            .ToList();
        
        var uniqueActiveDaysCount = activities.Select(a => a.Activity.StartTime.Date).Distinct().Count();

        var bodyBatteryDepletion = activities.Sum(a => 
            Math.Max(0, a.Activity.BeginningBodyBattery - a.Activity.EndingBodyBattery));
        
        var bodyBatteryEfficiency = bodyBatteryDepletion > 0 
            ? activities.Sum(a => a.Activity.MovingTime) / bodyBatteryDepletion 
            : 0;
        
        var ordered = activities
            .OrderBy(a => a.Activity.StartTime)
            .ToList();
        
        var firstVo2 = ordered.First().Activity.UserVolumetricOxygenMax;
        var lastVo2 = ordered.Last().Activity.UserVolumetricOxygenMax;
        
        var vo2Diff = lastVo2 - firstVo2;

        var vo2DiffPercent = firstVo2 > 0
            ? (vo2Diff / firstVo2) * 100
            : 0;
        
        var totalMovingTime = activities.Sum(a => a.Activity.MovingTime);

        var heartRateIntensity = totalMovingTime > 0
            ? activities.Sum(a =>
                  (a.ActivityMetrics.AverageHeartRate / (double)a.Activity.UserMaxHeartRate)
                  * a.Activity.MovingTime)
              / totalMovingTime
            : 0;
        
        var heartRateIntensityScore = Math.Round(heartRateIntensity * 100);
        
        var valid = runningActivities
            .Where(r =>
                r.ActivityMetrics is { StepLength: not null, AverageVerticalOscillation: not null, AverageStanceTime: not null, AverageVerticalRatio: not null })
            .ToList();
        
        var economyScore =
            valid.Any()
                ? valid.Average(r =>
                {
                    var step = NormalizeRunningStatistics(r.ActivityMetrics.StepLength ?? 0, 0.8, 1.5);
                    var osc  = NormalizeRunningStatistics(r.ActivityMetrics.AverageVerticalOscillation ?? 0, 6, 12, true);
                    var stance = NormalizeRunningStatistics(r.ActivityMetrics.AverageStanceTime ?? 0, 200, 350, true);
                    var ratio = NormalizeRunningStatistics(r.ActivityMetrics.AverageVerticalRatio ?? 0, 7, 12, true);

                    return (step * 0.35) + (osc * 0.25) + (stance * 0.2) + (ratio * 0.2);
                }) * 100
                : 0;
        
        return new WeeklyInsights
        {
            RunningTimeSeconds = runningActivities.Sum(a => a.Activity.TotalTime),
            RunningMovingTimeSeconds = runningActivities.Sum(a => a.Activity.MovingTime),
            RunningDistanceMetres = runningActivities.Sum(a => a.Activity.DistanceMetres),
            
            SwimmingTimeSeconds = swimmingActivities.Sum(a => a.Activity.TotalTime),
            SwimmingDistanceMetres = swimmingActivities.Sum(a => a.Activity.DistanceMetres),
            
            OtherTypes = otherActivityTypes,
            OtherTypesDistanceMetres = otherDistanceMetres,
            OtherTypesTimeSeconds = otherTotalTimeSeconds,
            
            TotalCaloriesBurned = activities.Sum(a => a.ActivityMetrics.TotalCalories),
            TotalTrainingScore = 
                activities.Sum(a => a.ActivityMetrics.AerobicTrainingEffect) + 
                activities.Sum(a=> a.ActivityMetrics.AnaerobicTrainingEffect),
            
            TrainingConsistencyScore = CalculateTrainingConsistencyScore(activities),
            
            VolumetricOxygenMaxTrend = vo2Diff,
            
            VolumetricOxygenMaxDiffPercent = vo2DiffPercent,
            
            BodyBatteryDepletion = bodyBatteryDepletion,
            BodyBatteryEfficiency = Math.Round(bodyBatteryEfficiency, 2),
            RecoveryTimeGenerated = activities.Sum(a => a.Activity.RecoveryTime),
            RunningEconomyIndex = economyScore,
            HeartRateIntensityScore = heartRateIntensityScore,
            
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
    
    private static double NormalizeRunningStatistics(double value, double min, double max, bool inverse = false)
    {
        var t = (value - min) / (max - min);
        t = Math.Clamp(t, 0, 1);
        return inverse ? 1 - t : t;
    }

    private static double CalculateTrainingConsistencyScore(IEnumerable<InsightsActivity> activities)
    {
        var activeDates = activities
            .Select(a => a.Activity.StartTime.Date)
            .Distinct()
            .OrderBy(d => d) 
            .ToList();

        var uniqueActiveDaysCount = activeDates.Count;
        var dayConsistencyScore = activeDates.Count == 0 ? 0 : Math.Min(1.0, uniqueActiveDaysCount / 7.0);
        
        double distributionScore;

        switch (activeDates.Count)
        {
            case > 1:
            {
                var gaps = new List<double>();
                for (var i = 1; i < activeDates.Count; i++)
                {
                    gaps.Add((activeDates[i] - activeDates[i - 1]).TotalDays);
                }

                var avgGap = gaps.Average();
                var gapVariance = gaps.Average(g => Math.Pow(g - avgGap, 2));
                var gapStdDev = Math.Sqrt(gapVariance);
    
                var coefficientOfVariation = avgGap == 0 ? 0 : gapStdDev / avgGap;
                distributionScore = 1.0 / (1.0 + coefficientOfVariation);
                break;
            }
            case 1:
                distributionScore = 0.5;
                break;
            default:
                distributionScore = 0;
                break;
        }

        return Math.Round(
            (0.7 * dayConsistencyScore) + 
            (0.3 * distributionScore), 3);
    }
}