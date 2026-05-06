using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Database.Entities;

namespace MyAIRunningMate.Database.Mappings;

public static class ActivityEntityMapper
{
    public static ActivityEntity ToActivityEntity(this Activity activity, Guid? stravaResourceId) => new()
    {
        GarminActivityId = activity.GarminActivityId,
        StartTime = activity.StartTime,
        ExerciseType = activity.ExerciseType,
        DurationSeconds = activity.DurationSeconds,
        DistanceMetres = activity.DistanceMetres,
        AverageHeartRate = activity.AverageHeartRate,
        MaxHeartRate = activity.MaxHeartRate,
        TotalElevationGain = activity.TotalElevationGain,
        AverageSecondPerKilometre = activity.AverageSecondPerKilometre,
        TrainingEffect = activity.TrainingEffect,
        StravaResourceId = stravaResourceId,
    };
}