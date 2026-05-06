using MyAIRunningMate.Database.Entities;

namespace MyAIRunningMate.Application.Models.ViewObjects;

public static class ActivityViewMapper
{
    public static ActivityView ToActivityView(this ActivityEntity entity) => new()
    {
        ActivityId = entity.ActivityId,
        GarminActivityId =  entity.GarminActivityId,
        StartTime = entity.StartTime,
        ExerciseType = entity.ExerciseType,
        DurationSeconds = entity.DurationSeconds,
        DistanceMetres = entity.DistanceMetres,
        AverageHeartRate = entity.AverageHeartRate,
        MaxHeartRate = entity.MaxHeartRate,
        TotalElevationGain = entity.TotalElevationGain,
        TrainingEffect = entity.TrainingEffect,
        AverageSecondPerKilometre = entity.AverageSecondPerKilometre,
    };
}