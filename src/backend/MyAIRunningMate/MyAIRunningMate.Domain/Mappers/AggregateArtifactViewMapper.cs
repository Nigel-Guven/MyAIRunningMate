using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Mappers;

public static class AggregateArtifactViewMapper
{
    public static AggregateArtifactViewDto ToDto(Activity garminActivity,
        StravaResource stravaActivity) => new()
    {
        ActivityId = garminActivity?.ActivityId ?? null,
        GarminActivityId = garminActivity.GarminActivityId,
        StartTime = garminActivity.StartTime,
        DistanceMetres = garminActivity.DistanceMetres,
        DurationSeconds = garminActivity.DurationSeconds,
        TrainingEffect = garminActivity.TrainingEffect,
        AverageSecondPerKilometre = garminActivity.AverageSecondPerKilometre,
        AverageHeartRate = garminActivity.AverageHeartRate,
        MaxHeartRate = garminActivity.MaxHeartRate,
        Laps = garminActivity.Laps,
            
        ResourceId = stravaActivity?.ResourceId ?? Guid.Empty,
        StravaId = stravaActivity?.StravaId,
        Name = stravaActivity?.Name ?? "Unnamed Activity",
            
        ExerciseType = stravaActivity?.Type ?? garminActivity.ExerciseType ?? "Unknown",

        StartDate = stravaActivity?.StartDate ?? garminActivity.StartTime,
        

        ElapsedTime = stravaActivity?.ElapsedTime,
        AverageCadence = stravaActivity?.AverageCadence,
        TotalElevationGain = garminActivity.TotalElevationGain ?? stravaActivity?.TotalElevationGain ?? 0.0,
        ElevationLow = stravaActivity?.ElevationLow,
        ElevationHigh = stravaActivity?.ElevationHigh,

        AchievementCount = stravaActivity?.AchievementCount ?? 0,
        KudosCount = stravaActivity?.KudosCount ?? 0,
        PersonalRecordCount = stravaActivity?.PersonalRecordCount ?? 0,
        AthleteCount = stravaActivity?.AthleteCount ?? 0,
        
        Map = stravaActivity?.StravaGeomap
    };
}