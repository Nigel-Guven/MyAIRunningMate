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
            DurationSeconds: model.GarminActivity.TotalTime,
            MovingTimeSeconds: model.GarminActivity.MovingTime,
            DistanceMetres: model.GarminActivity.DistanceMetres,
            Calories: model.GarminActivityMetrics.TotalCalories,
            AverageHeartRate: model.GarminActivityMetrics.AverageHeartRate,
            MaxHeartRate: model.GarminActivityMetrics.MaxHeartRate,
            TotalElevationGain: model.GarminActivity.TotalAscent,
            TrainingEffect: model.GarminActivityMetrics.AerobicTrainingEffect,
            PoolLength: model.GarminActivityMetrics.PoolLength,
            MapPolyline: model.GarminActivity.MapPolyline,
            TimeSeriesRecords: model.TimeSeriesRecords?.Select(tsr => tsr.ToTimeSeriesRecordResponseDto()).ToList(),
            Laps: model.Laps.Select(l => l.ToLapDto()).ToList()
        );

    private static TimeSeriesRecordResponse ToTimeSeriesRecordResponseDto(this TimeSeriesRecord model) => 
        new()
        {
            Timestamp = model.Timestamp,
            DistanceMetres = model.DistanceMetres,
            HeartRate = model.HeartRate,
            Cadence = model.Cadence,
            Power = model.Power,
            Latitude = model.Latitude,
            Longitude = model.Longitude
        };
    
    private static LapViewResponse ToLapDto(this Lap model) => 
        new(
            LapNumber: model.LapNumber,
            LapStartTime: model.LapStartTime,
            DistanceMetres: model.DistanceMetres,
            DurationSeconds: model.DurationSeconds,
            AverageHeartRate: model.AverageHeartRate,
            MaxHeartRate: model.MaxHeartRate,
            AverageSpeed: model.AverageSpeed,
            AverageCadence: model.AverageCadence,
            PrimaryStroke: model.PrimaryStroke,
            NumberOfLengths: model.NumberOfLengths
        );
}
