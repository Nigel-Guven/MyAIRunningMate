namespace MyAIRunningMate.Contracts.Aggregates.Responses;

public record ActivityMetricsResponse(
    int TotalCycles,
    int TotalCalories, 
    int? EstimatedSweatLoss,
    int? AverageTemperature,
    int? MaxTemperature,
    int AverageHeartRate,
    int MaxHeartRate,
    int? AveragePower,
    int? MaxPower,
    int AverageCadence,
    int? MaxCadence,
    double? AverageVerticalOscillation,
    double? StepLength,
    double? AverageVerticalRatio,
    double? AverageStanceTime,
    double AerobicTrainingEffect,
    double AnaerobicTrainingEffect,
    int? AverageSwolf,
    int? PoolLength
    );