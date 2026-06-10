using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Contracts.Views;

namespace MyAIRunningMate.Service.Mappers;

public static class WeeklyInsightsDtoMapper
{
    public static WeeklyInsightsDto ToWeeklyInsightsDto(this WeeklyInsights model) => new()
    {
        RunningTimeVolume =  model.RunningTimeVolume,
        RunningDistanceVolume =  model.RunningDistanceVolume,
        SwimmingTimeVolume = model.SwimmingTimeVolume,
        SwimmingDistanceVolume = model.SwimmingDistanceVolume,
        TotalRunningElevationGain =  model.TotalRunningElevationGain,
        MeanAverageHeartRate = model.MeanAverageHeartRate,
        MeanMaxHeartRate = model.MeanMaxHeartRate,
        TotalTrainingEffect = model.TotalTrainingEffect,
        MeanTrainingEffect = model.MeanTrainingEffect,
        TotalAchievementCount = model.TotalAchievementCount,
        TotalPersonalRecordCount = model.TotalPersonalRecordCount,
        TotalPersonalExercises = model.TotalPersonalExercises,
        TotalGroupExercises = model.TotalGroupExercises,
        Locations = model.Locations,
        RestDays = model.RestDays,
    };
}