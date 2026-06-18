using MyAIRunningMate.Contracts.Analytics.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class AnalyticsDtoExtensions
{
    public static WeeklyInsightsResponse ToWeeklyInsightsDto(this WeeklyInsights model) =>
        new(
            RunningTimeSeconds: model.RunningTimeSeconds,
            RunningMovingTimeSeconds: model.RunningMovingTimeSeconds,
            RunningDistanceMetres: model.RunningDistanceMetres,
            TotalRunningElevationGain: model.TotalRunningElevationGain,
            SwimmingTimeSeconds: model.SwimmingTimeSeconds,
            SwimmingDistanceMetres: model.SwimmingDistanceMetres,
            TotalCaloriesBurned: model.TotalCaloriesBurned,
            MeanAverageHeartRate: model.MeanAverageHeartRate,
            MeanMaxHeartRate: model.MeanMaxHeartRate,
            TotalTrainingEffect: model.TotalTrainingEffect,
            MeanTrainingEffect: model.MeanTrainingEffect,
            MorningActivities: model.MorningActivities,
            AfternoonActivities: model.AfternoonActivities,
            EveningActivities: model.EveningActivities,
            NightActivities: model.NightActivities,
            Locations: model.Locations,
            RestDays: model.RestDays,
            RunningTimeBreakSeconds: model.RunningTimeBreakSeconds,
            ElevationIntensity: model.ElevationIntensity,
            CaloricIntensity: model.CaloricIntensity,
            RunningMovingEfficiency: model.RunningMovingEfficiency
        );

    public static YearlyStatisticsResponse ToYearlyStatisticsDto(this YearlyStatistics model) =>
        new(
            YearlyRunningDistance: model.YearlyRunningDistance,
            YearlySwimmingDistance: model.YearlySwimmingDistance,
            YearlyActiveDays: model.YearlyActiveDays,
            YearlyAverageTrainingEffect: model.YearlyAverageTrainingEffect,
            YearlyTotalTrainingEffect: model.YearlyTotalTrainingEffect
        );
}