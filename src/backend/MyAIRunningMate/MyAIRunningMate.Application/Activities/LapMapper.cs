using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Client.Python.Responses;

namespace MyAIRunningMate.Application.Activities;

public static class LapMapper
{
    public static Lap ToLap(this PythonApiLap response) => new()
    {
        LapNumber = response.LapNumber,
        Distance = response.Distance,
        Duration = response.Duration,
        AverageHeartRate = response.AverageHeartRate,
    };
}