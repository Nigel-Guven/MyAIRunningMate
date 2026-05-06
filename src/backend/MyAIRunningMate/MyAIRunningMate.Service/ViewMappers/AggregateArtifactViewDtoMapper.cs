using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Contracts.Views;

namespace MyAIRunningMate.Service.ViewMappers;

public static class AggregateArtifactViewDtoMapper
{
    public static AggregateArtifactViewDto ToAggregateArtifactViewDto(
        AggregateArtifactView artifactView) => new()
    {
        ActivityId = artifactView.ActivityId,
        GarminActivityId = artifactView.GarminActivityId,
        StartTime = artifactView.StartTime,
        DistanceMetres = artifactView.DistanceMetres?? 0.0,
        DurationSeconds = artifactView.DurationSeconds,
        TrainingEffect = artifactView.TrainingEffect ?? 0.0,
        AverageSecondPerKilometre = artifactView.AverageSecondPerKilometre ?? 0.0,
        AverageHeartRate = artifactView.AverageHeartRate,
        MaxHeartRate = artifactView.MaxHeartRate,
        Laps = artifactView.Laps.Select(lapView => lapView.ToLapViewDto()),
            
        ResourceId = artifactView?.ResourceId ?? Guid.Empty,
        StravaId = artifactView?.StravaId,
        Name = artifactView?.Name ?? "Unnamed Activity",
            
        ExerciseType = artifactView.ExerciseType ?? "Unknown",

        ElapsedTime = artifactView?.ElapsedTime,
        AverageCadence = artifactView?.AverageCadence,
        TotalElevationGain = artifactView.TotalElevationGain ?? 0.0,
        ElevationLow = artifactView?.ElevationLow,
        ElevationHigh = artifactView?.ElevationHigh,

        AchievementCount = artifactView?.AchievementCount ?? 0,
        KudosCount = artifactView?.KudosCount ?? 0,
        PersonalRecordCount = artifactView?.PersonalRecordCount ?? 0,
        AthleteCount = artifactView?.AthleteCount ?? 0,
        
        Map = artifactView.Map.ToMapViewDto()
    };
}