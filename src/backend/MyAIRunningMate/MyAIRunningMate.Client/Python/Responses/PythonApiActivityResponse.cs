using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses;

public record PythonApiActivityResponse(
    [property: JsonPropertyName("garmin_id")] 
    string GarminId,
    
    [property: JsonPropertyName("start_time")] 
    DateTime StartTime,
    
    [property: JsonPropertyName("type")] 
    string Type,
    
    [property: JsonPropertyName("duration_seconds")] 
    double DurationSeconds,
    
    [property: JsonPropertyName("moving_time_seconds")] 
    double MovingTimeSeconds,
    
    [property: JsonPropertyName("distance_metres")] 
    double DistanceMetres,
    
    [property: JsonPropertyName("calories")] 
    int Calories,
    
    [property: JsonPropertyName("average_heart_rate")] 
    int AverageHeartRate,
    
    [property: JsonPropertyName("max_heart_rate")] 
    int MaxHeartRate,
    
    [property: JsonPropertyName("total_elevation_gain")] 
    double TotalElevationGain,
    
    [property: JsonPropertyName("training_effect")] 
    double TrainingEffect,
    
    [property: JsonPropertyName("raw_pace_seconds_per_metre")] 
    double? RawPaceSecondsPerMetre,
    
    [property: JsonPropertyName("detected_pool_length")] 
    int? PoolLength,
    
    [property: JsonPropertyName("laps")] 
    IEnumerable<PythonApiLap> Laps,
    
    [property: JsonPropertyName("time_series")] 
    IEnumerable<PythonApiTimeSeries> TimeSeries
);