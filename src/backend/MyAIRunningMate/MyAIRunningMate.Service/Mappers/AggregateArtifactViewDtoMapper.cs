using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Contracts.Aggregates.Responses;

namespace MyAIRunningMate.Service.Mappers;

public static class AggregateArtifactViewDtoMapper
{
    public static AggregateArtifactResponse ToAggregateArtifactViewDto(
        AggregateArtifactView model) => new()
    {
        ActivityId = model.ActivityId,
        GarminActivityId = model.GarminActivityId,
        StartTime = model.StartTime,
        DistanceMetres = model.DistanceMetres?? 0.0,
        DurationSeconds = model.DurationSeconds,
        TrainingEffect = model.TrainingEffect ?? 0.0,
        AverageSecondPerKilometre = model.AverageSecondPerKilometre ?? 0.0,
        AverageHeartRate = model.AverageHeartRate,
        MaxHeartRate = model.MaxHeartRate,
        Laps = model.Laps.Select(lapView => lapView.ToLapViewDto()),
            
        ResourceId = model?.ResourceId ?? Guid.Empty,
        StravaId = model?.StravaId,
        Name = model?.Name ?? "Unnamed Activity",
            
        ExerciseType = model.ExerciseType ?? "Unknown",

        ElapsedTime = model?.ElapsedTime,
        AverageCadence = model?.AverageCadence,
        TotalElevationGain = model.TotalElevationGain ?? 0.0,
        ElevationLow = model?.ElevationLow,
        ElevationHigh = model?.ElevationHigh,

        AchievementCount = model?.AchievementCount ?? 0,
        KudosCount = model?.KudosCount ?? 0,
        PersonalRecordCount = model?.PersonalRecordCount ?? 0,
        AthleteCount = model?.AthleteCount ?? 0,
        
        Map = model.Map?.ToMapViewDto()
    };
}