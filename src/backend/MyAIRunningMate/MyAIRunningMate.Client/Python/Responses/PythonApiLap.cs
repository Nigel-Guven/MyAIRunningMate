using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses;

public record PythonApiLap(
    [property: JsonPropertyName("lap")]
    int LapNumber,
    
    [property: JsonPropertyName("distance_metres")] 
    double DistanceMetres,
    
    [property: JsonPropertyName("duration_seconds")] 
    double DurationSeconds,
    
    [property: JsonPropertyName("average_heart_rate")] 
    int AverageHeartRate,
    
    [property: JsonPropertyName("average_speed")] 
    double AverageSpeed,
    
    [property: JsonPropertyName("average_cadence")] 
    int AverageCadence,
    
    [property: JsonPropertyName("primary_stroke")] 
    string? PrimaryStroke,
    
    [property: JsonPropertyName("average_swolf")] 
    int? AverageSwolf
);