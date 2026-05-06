using MyAIRunningMate.Client.Python.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Mappers;

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