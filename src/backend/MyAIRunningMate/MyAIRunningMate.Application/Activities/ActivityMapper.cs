using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Client.Python.Responses;

namespace MyAIRunningMate.Application.Activities;

public static class ActivityMapper
{
    public static Activity ToActivity(this PythonApiActivityResponse response) => new()
    {
        GarminActivityId = response.GarminId,
        StartTime = response.StartTime,
        ExerciseType = response.Type,
        DurationSeconds = response.DurationSeconds,
        DistanceMetres = response.DistanceMetres,
        AverageHeartRate = response.AverageHeartRate,
        MaxHeartRate = response.MaxHeartRate,
        TotalElevationGain = response.TotalElevationGain,
        TrainingEffect = response.TrainingEffect,
        AverageSecondPerKilometre = response.AverageSecondPerKilometre,
        Laps = response.Laps.Select(rl => rl.ToLap()),
    };
}