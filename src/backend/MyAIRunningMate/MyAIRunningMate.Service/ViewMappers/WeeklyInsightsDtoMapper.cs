using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Contracts.Views;

namespace MyAIRunningMate.Service.ViewMappers;

public static class WeeklyInsightsDtoMapper
{
    public static WeeklyInsightsDto ToWeeklyInsightsDto(this WeeklyInsights entity) => new()
    {
        RunningTimeVolume =  entity.RunningTimeVolume,
        RunningDistanceVolume =  entity.RunningDistanceVolume,
        SwimmingTimeVolume = entity.SwimmingTimeVolume,
        SwimmingDistanceVolume = entity.SwimmingDistanceVolume,
        TotalRunningElevationGain =  entity.TotalRunningElevationGain,
        MeanAverageHeartRate = entity.MeanAverageHeartRate,
        MeanMaxHeartRate = entity.MeanMaxHeartRate,
        TotalTrainingEffect = entity.TotalTrainingEffect,
        MeanTrainingEffect = entity.MeanTrainingEffect,
        TotalAchievementCount = entity.TotalAchievementCount,
        TotalPersonalRecordCount = entity.TotalPersonalRecordCount,
        TotalPersonalExercises = entity.TotalPersonalExercises,
        TotalGroupExercises = entity.TotalGroupExercises,
        Locations = entity.Locations,
        RestDays = entity.RestDays,
    };
}