using MyAIRunningMate.Contracts.Aggregates.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class AggregateDtoExtensions
{
    public static AggregateArtifactResponse ToAggregateArtifactDto(this AggregateArtifact model) => 
        new(
            ActivityId: model.GarminActivity.ActivityId,
            GarminActivityId: model.GarminActivity.GarminActivityId,
            StartTime: model.GarminActivity.StartTime,
            ExerciseType: model.GarminActivity.ExerciseType,
            DurationSeconds: model.GarminActivity.DurationSeconds,
            MovingTimeSeconds: model.GarminActivity.MovingTimeSeconds,
            DistanceMetres: model.GarminActivity.DistanceMetres,
            Calories: model.GarminActivity.Calories,
            AverageHeartRate: model.GarminActivity.AverageHeartRate,
            MaxHeartRate: model.GarminActivity.MaxHeartRate,
            TotalElevationGain: model.GarminActivity.TotalElevationGain,
            RawPaceSecondsPerMetre: model.GarminActivity.RawPaceSecondsPerMetre,
            TrainingEffect: model.GarminActivity.TrainingEffect,
            PoolLength: model.GarminActivity.PoolLength,
            MapPolyline: model.GarminActivity.MapPolyline,
            TimeSeriesRecords: model.GarminActivity.TimeSeriesRecords?.Select(tsr => tsr.ToTimeSeriesRecordResponseDto()).ToList(),
            Laps: model.Laps.Select(l => l.ToLapDto()).ToList()
        );

    private static TimeSeriesRecordResponse ToTimeSeriesRecordResponseDto(this TimeSeriesRecord model) => 
        new(
            Timestamp: model.Timestamp,
            DistanceMetres: model.DistanceMetres,
            HeartRate: model.HeartRate,
            Cadence: model.Cadence,
            Latitude: model.Latitude,
            Longitude: model.Longitude
        );
    
    private static LapViewResponse ToLapDto(this Lap model) => 
        new(
            LapNumber: model.LapNumber,
            DistanceMetres: model.DistanceMetres,
            DurationSeconds: model.DurationSeconds,
            AverageHeartRate: model.AverageHeartRate,
            AverageSpeed: model.AverageSpeed,
            AverageCadence: model.AverageCadence,
            PrimaryStroke: model.PrimaryStroke,
            AverageSwolf: model.AverageSwolf
        );
}
