namespace MyAIRunningMate.Application.Models.ViewObjects;

public static class IngestionViewMapper
{
    public static IngestionView ToIngestionView(this Activity activity) => new()
    {
        GarminActivityId = activity.GarminActivityId,
        StartTime = activity.StartTime,
        ExerciseType = activity.ExerciseType,
        DurationSeconds = activity.DurationSeconds,
        DistanceMetres = activity.DistanceMetres,
        TrainingEffect = activity.TrainingEffect,
    };
}