using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses;

public record PythonApiLap(
    [property: JsonPropertyName("lap")]
    int LapNumber,
    
    [property: JsonPropertyName("distance_metres")] 
    double Distance,
    
    [property: JsonPropertyName("duration_seconds")] 
    double Duration,
    
    [property: JsonPropertyName("average_heart_rate")] 
    int AverageHeartRate
);