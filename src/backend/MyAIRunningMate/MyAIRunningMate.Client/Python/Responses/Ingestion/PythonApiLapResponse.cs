using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses.Ingestion;

public record PythonApiLapResponse(
    [property: JsonPropertyName("lap_number")]
    int LapNumber,
    
    [property: JsonPropertyName("lap_start_time")]
    DateTime LapStartTime,
    
    [property: JsonPropertyName("lap_distance_metres")] 
    double LapDistanceMetres,
    
    [property: JsonPropertyName("lap_duration_seconds")] 
    double LapDurationSeconds,
    
    [property: JsonPropertyName("lap_average_heart_rate")] 
    int LapAverageHeartRate,
    
    [property: JsonPropertyName("lap_max_heart_rate")] 
    int LapMaxHeartRate,
    
    [property: JsonPropertyName("lap_average_speed")] 
    double LapAverageSpeed,
    
    [property: JsonPropertyName("lap_average_cadence")] 
    int LapAverageCadence,
    
    [property: JsonPropertyName("lap_swim_stroke")] 
    string? LapPrimaryStroke,
    
    [property: JsonPropertyName("lap_num_lengths")] 
    int? LapNumberOfLengths
);