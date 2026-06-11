using System.Text.Json.Serialization;
using MyAIRunningMate.Client.Python.Responses;

public record PythonApiActivityResponse(
    [property: JsonPropertyName("garmin_id")] 
    string GarminId,
    
    [property: JsonPropertyName("start_time")] 
    DateTime StartTime,
    
    [property: JsonPropertyName("type")] 
    string Type,
    
    [property: JsonPropertyName("duration_seconds")] 
    double DurationSeconds,
    
    [property: JsonPropertyName("distance_metres")] 
    double DistanceMetres,
    
    [property: JsonPropertyName("average_heart_rate")] 
    int AverageHeartRate,
    
    [property: JsonPropertyName("max_heart_rate")] 
    int MaxHeartRate,
    
    [property: JsonPropertyName("total_elevation_gain")] 
    double? TotalElevationGain,
    
    [property: JsonPropertyName("training_effect")] 
    double TrainingEffect,
    
    [property: JsonPropertyName("average_pace_seconds_per_kilometre")] 
    double AverageSecondPerKilometre,
    
    [property: JsonPropertyName("laps")] 
    IEnumerable<PythonApiLap> Laps
);