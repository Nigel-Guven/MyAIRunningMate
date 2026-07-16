using MyAIRunningMate.Contracts.Aggregates.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class AggregateDtoExtensions
{
    public static AggregateArtifactResponse ToAggregateArtifactDto(this AggregateArtifact model) => 
        new(
            ActivityDetails: model.GarminActivity.ToActivityDetailsDto(),
            ActivityMetrics: model.GarminActivityMetrics.ToActivityMetricsDto(),
            TimeSeriesRecords: model.TimeSeriesRecords?.Select(tsr => tsr.ToTimeSeriesRecordResponseDto()).ToList(),
            Laps: model.Laps.Select(l => l.ToLapDto()).ToList(),
            BestEfforts: model.BestEfforts?.Select(be => be.ToBestEffortResponse()).ToList()
        );

    private static ActivityDetailsResponse ToActivityDetailsDto(this Activity model) => 
        new(
            ActivityId: model.ActivityId,
            GarminActivityId: model.GarminActivityId,
            StartTime: model.StartTime,
            TotalTime: model.TotalTime,
            MovingTime: model.MovingTime,
            DistanceMetres: model.DistanceMetres,
            BeginningBodyBattery: model.BeginningBodyBattery,
            BeginningBodyPotential: model.BeginningBodyPotential,
            EndingBodyBattery: model.EndingBodyBattery,
            EndingPotential: model.EndingPotential,
            TotalAscent: model.TotalAscent,
            TotalDescent: model.TotalDescent,
            RecoveryTime: model.RecoveryTime,
            ExerciseType: model.ExerciseType,
            ExerciseSubType: model.ExerciseSubType,
            ExerciseName: model.ExerciseName,
            UserVolumetricOxygenMax: model.UserVolumetricOxygenMax,
            UserMaxHeartRate: model.UserMaxHeartRate,
            UserLactateThresholdHeartRate: model.UserLactateThresholdHeartRate,
            UserLactateThresholdPower: model.UserLactateThresholdPower,
            UserLactateThresholdSpeed: model.UserLactateThresholdSpeed,
            NumberOfLaps: model.NumberOfLaps,
            Location: model.Location,
            MapPolyline: model.MapPolyline
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
    
    private static ActivityMetricsResponse ToActivityMetricsDto(this ActivityMetrics model) => 
        new(
            TotalCycles: model.TotalCycles,
            TotalCalories: model.TotalCalories,
            EstimatedSweatLoss: model.EstimatedSweatLoss,
            AverageTemperature: model.AverageTemperature,
            MaxTemperature: model.MaxTemperature,
            AverageHeartRate: model.AverageHeartRate,
            MaxHeartRate: model.MaxHeartRate,
            AveragePower: model.AveragePower,
            MaxPower: model.MaxPower,
            AverageCadence: model.AverageCadence,
            MaxCadence: model.MaxCadence,
            AverageVerticalOscillation: model.AverageVerticalOscillation,
            StepLength: model.StepLength,
            AverageVerticalRatio: model.AverageVerticalRatio,
            AverageStanceTime: model.AverageStanceTime,
            AerobicTrainingEffect: model.AerobicTrainingEffect,
            AnaerobicTrainingEffect: model.AnaerobicTrainingEffect,
            AverageSwolf: model.AverageSwolf,
            PoolLength: model.PoolLength
        );
}
