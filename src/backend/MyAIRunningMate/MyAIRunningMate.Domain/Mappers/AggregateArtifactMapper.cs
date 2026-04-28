using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Mappers;

public static class AggregateArtifactMapper
{
    public static AggregateArtifactDto ToDto(ActivityDto garminActivityDto,
        StravaResourceDto stravaActivityDto) => new()
    {
        ActivityId = garminActivityDto.ActivityId,
        GarminActivityId = garminActivityDto.GarminActivityId,
        StartTime = garminActivityDto.StartTime,
        DistanceMetres = garminActivityDto.DistanceMetres,
        DurationSeconds = garminActivityDto.DurationSeconds,
        TrainingEffect = garminActivityDto.TrainingEffect,
        AverageSecondPerKilometre = garminActivityDto.AverageSecondPerKilometre,
        AverageHeartRate = garminActivityDto.AverageHeartRate,
        MaxHeartRate = garminActivityDto.MaxHeartRate,
        Laps = garminActivityDto.Laps,
            
        ResourceId = stravaActivityDto?.ResourceId ?? Guid.Empty,
        StravaId = stravaActivityDto?.StravaId,
        Name = stravaActivityDto?.Name ?? "Unnamed Activity",
            
        ExerciseType = stravaActivityDto?.Type ?? garminActivityDto.ExerciseType ?? "Unknown",

        StartDate = stravaActivityDto?.StartDate ?? garminActivityDto.StartTime,
        

        ElapsedTime = stravaActivityDto?.ElapsedTime,
        AverageCadence = stravaActivityDto?.AverageCadence,
        TotalElevationGain = garminActivityDto.TotalElevationGain ?? stravaActivityDto?.TotalElevationGain ?? 0.0,
        ElevationLow = stravaActivityDto?.ElevationLow,
        ElevationHigh = stravaActivityDto?.ElevationHigh,

        AchievementCount = stravaActivityDto?.AchievementCount ?? 0,
        KudosCount = stravaActivityDto?.KudosCount ?? 0,
        PersonalRecordCount = stravaActivityDto?.PersonalRecordCount ?? 0,
        AthleteCount = stravaActivityDto?.AthleteCount ?? 0,
        
        Map = stravaActivityDto?.StravaGeomap
    };
}