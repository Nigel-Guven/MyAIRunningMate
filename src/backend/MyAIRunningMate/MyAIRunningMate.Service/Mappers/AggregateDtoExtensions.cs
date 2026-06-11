using MyAIRunningMate.Contracts.Aggregates.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class AggregateDtoExtensions
{
    public static AggregateArtifactResponse ToAggregateArtifactDto(this AggregateArtifact model)
    {
        return new AggregateArtifactResponse(
            ActivityId: model.ActivityId,
            ResourceId: model.ResourceId,
            GarminActivityId: model.GarminActivityId,
            StravaId: model.StravaId,
            Name: model.Name,
            ExerciseType: model.ExerciseType,
            StartTime: model.StartTime,
            ElapsedTime: model.ElapsedTime,
            AverageCadence: model.AverageCadence,
            AverageSecondPerKilometre: model.AverageSecondPerKilometre,
            TotalElevationGain: model.TotalElevationGain,
            ElevationLow: model.ElevationLow,
            ElevationHigh: model.ElevationHigh,
            DurationSeconds: model.DurationSeconds,
            DistanceMetres: model.DistanceMetres,
            AverageHeartRate: model.AverageHeartRate,
            MaxHeartRate: model.MaxHeartRate,
            TrainingEffect: model.TrainingEffect,
            AchievementCount: model.AchievementCount,
            KudosCount: model.KudosCount,
            AthleteCount: model.AthleteCount,
            PersonalRecordCount: model.PersonalRecordCount,
            Map: model.Map?.ToStravaGeomapDto(),
            Laps: model.Laps.Select(lap => lap.ToLapViewDto())
        );
    }

    public static LapViewResponse ToLapViewDto(this LapArtifact model)
    {
        return new LapViewResponse(
            LapNumber: model.LapNumber,
            DistanceMetres: model.DistanceMetres,
            DurationSeconds: model.DurationSeconds,
            AverageHeartRate: model.AverageHeartRate
        );
    }

    public static StravaGeomapViewDto ToStravaGeomapDto(this StravaGeomap model)
    {
        return new StravaGeomapViewDto(
            MapPolyline: model.MapPolyline
        );
    }
}