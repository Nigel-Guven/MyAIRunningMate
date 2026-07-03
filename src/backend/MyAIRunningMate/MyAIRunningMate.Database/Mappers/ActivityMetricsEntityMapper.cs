using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class ActivityMetricsEntityMapper
{
    public static ActivityMetrics ToDomain(this ActivityMetricsEntity entity) =>
        new(
            activityId: entity.ActivityId,
            totalCycles: entity.TotalCycles,
            totalCalories: entity.TotalCalories,
            averageHeartRate: entity.AverageHeartRate,
            maxHeartRate: entity.MaxHeartRate,
            averageCadence: entity.AverageCadence,
            aerobicTrainingEffect: entity.AerobicTrainingEffect,
            anaerobicTrainingEffect: entity.AerobicTrainingEffect,
            estimatedSweatLoss: entity.EstimatedSweatLoss,
            averageTemperature: entity.AverageTemperature,
            maxTemperature: entity.MaxTemperature,
            averagePower: entity.AveragePower,
            maxPower: entity.MaxPower,
            maxCadence: entity.MaxCadence,
            averageVerticalOscillation: entity.AverageVerticalOscillation,
            stepLength: entity.StepLength,
            averageVerticalRatio: entity.AverageVerticalRatio,
            averageStanceTime: entity.AverageStanceTime,
            averageSwolf: entity.AverageSwolf,
            poolLength: entity.PoolLength
        );

    public static ActivityMetricsEntity ToEntity(this ActivityMetrics domain) =>
        new()
        {
            ActivityId = domain.ActivityId,
            TotalCycles = domain.TotalCycles,
            TotalCalories = domain.TotalCalories,
            EstimatedSweatLoss = domain.EstimatedSweatLoss,
            AverageTemperature =  domain.AverageTemperature,
            MaxTemperature = domain.MaxTemperature,
            AverageHeartRate = domain.AverageHeartRate,
            MaxHeartRate = domain.MaxHeartRate,
            AveragePower =  domain.AveragePower,
            MaxPower = domain.MaxPower,
            AverageCadence = domain.AverageCadence,
            MaxCadence = domain.MaxCadence,
            AverageVerticalOscillation = domain.AverageVerticalOscillation,
            StepLength = domain.StepLength,
            AverageVerticalRatio = domain.AverageVerticalRatio,
            AverageStanceTime = domain.AverageStanceTime,
            AerobicTrainingEffect =  domain.AerobicTrainingEffect,
            AnaerobicTrainingEffect =   domain.AnaerobicTrainingEffect,
            AverageSwolf = domain.AverageSwolf,
            PoolLength = domain.PoolLength
        };
}