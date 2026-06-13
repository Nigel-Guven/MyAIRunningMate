using MyAIRunningMate.Contracts.Analytics.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class AnalyticsDtoExtensions
{
    public static WeeklyInsightsResponse ToWeeklyInsightsDto(this WeeklyInsights model) =>
        new(
            RunningTimeVolume: model.RunningTimeVolume,
            RunningDistanceVolume: model.RunningDistanceVolume,
            SwimmingTimeVolume: model.SwimmingTimeVolume,
            SwimmingDistanceVolume: model.SwimmingDistanceVolume,
            TotalRunningElevationGain: model.TotalRunningElevationGain,
            MeanAverageHeartRate: model.MeanAverageHeartRate,
            MeanMaxHeartRate: model.MeanMaxHeartRate,
            TotalTrainingEffect: model.TotalTrainingEffect,
            MeanTrainingEffect: model.MeanTrainingEffect,
            TotalAchievementCount: model.TotalAchievementCount,
            TotalPersonalRecordCount: model.TotalPersonalRecordCount,
            TotalPersonalExercises: model.TotalPersonalExercises,
            TotalGroupExercises: model.TotalGroupExercises,
            Locations: model.Locations,
            RestDays: model.RestDays
        );

    public static WeeklyVolumeResponse ToWeeklyVolumeDto(this WeeklyVolume model) =>
        new(
            AverageMaxHeartRate: model.AverageMaxHeartRate,
            AverageTrainingEffectThisWeek: model.AverageTrainingEffectThisWeek,
            RunningTimeThisWeek: model.RunningTimeThisWeek,
            RunningDistanceLastWeek: model.RunningDistanceLastWeek,
            RunningDistanceThisWeek: model.RunningDistanceThisWeek,
            PlannedRunningDistanceLastWeek: model.PlannedRunningDistanceLastWeek,
            PlannedRunningDistanceThisWeek: model.PlannedRunningDistanceThisWeek,
            TotalElevationGainThisWeek: model.TotalElevationGainThisWeek,
            SwimmingDistanceThisWeek: model.SwimmingDistanceThisWeek
        );

    public static YearlyAnalyticsResponse ToYearlyAnalyticsDto(this YearlyAnalytics model) =>
        new(
            Summary: model.Summary.ToYearlyStatisticsDto(),
            WeeklyVolumes: model.WeeklyVolumes.Select(v => v.ToWeeklyInsightsDto())
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