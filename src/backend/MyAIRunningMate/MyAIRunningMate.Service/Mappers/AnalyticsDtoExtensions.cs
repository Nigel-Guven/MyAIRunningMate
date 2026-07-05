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
            SwimmingTimeSeconds: model.SwimmingTimeSeconds,
            SwimmingDistanceMetres: model.SwimmingDistanceMetres,
            OtherTypes: model.OtherTypes,
            OtherTypesDistanceMetres: model.OtherTypesDistanceMetres,
            OtherTypesTimeSeconds: model.OtherTypesTimeSeconds,
            TotalCaloriesBurned: model.TotalCaloriesBurned,
            TotalTrainingScore: model.TotalTrainingScore,
            TrainingConsistencyScore: model.TrainingConsistencyScore,
            MorningActivities: model.MorningActivities,
            AfternoonActivities: model.AfternoonActivities,
            EveningActivities: model.EveningActivities,
            NightActivities: model.NightActivities,
            Locations: model.Locations,
            RestDays: model.RestDays,
            RunningMovingEfficiency: model.RunningMovingEfficiency,
            PausedSeconds: model.PausedSeconds,
            BodyBatteryEfficiency: model.BodyBatteryEfficiency,
            BodyBatteryDepletion: model.BodyBatteryDepletion,
            RecoveryTimeGenerated: model.RecoveryTimeGenerated,
            HeartRateIntensityScore: model.HeartRateIntensityScore,
            VolumetricOxygenMaxTrend: model.VolumetricOxygenMaxTrend,
            VolumetricOxygenMaxDiffPercent: model.VolumetricOxygenMaxDiffPercent,
            RunningEconomyIndex: model.RunningEconomyIndex);

    public static YearlyStatisticsResponse ToYearlyStatisticsDto(this YearlyStatistics model) =>
        new(
            YearlyRunningDistance: model.YearlyRunningDistance,
            YearlySwimmingDistance: model.YearlySwimmingDistance,
            YearlyActiveDays: model.YearlyActiveDays,
            YearlyAverageTrainingEffect: model.YearlyAverageTrainingEffect,
            YearlyTotalTrainingEffect: model.YearlyTotalTrainingEffect
        );
}