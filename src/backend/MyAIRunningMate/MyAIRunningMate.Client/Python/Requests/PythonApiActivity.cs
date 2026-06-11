using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Requests;

public record PythonApiActivity(
    [property: JsonPropertyName("exercise_type")] 
    string ExerciseType,
    
    [property: JsonPropertyName("start_date")] 
    DateTime StartTime,
    
    [property: JsonPropertyName("duration_seconds")] 
    double DurationSeconds,
    
    [property: JsonPropertyName("distance_metres")] 
    double? DistanceMetres,
    
    [property: JsonPropertyName("average_heart_rate")] 
    int AverageHeartRate,
    
    [property: JsonPropertyName("max_heart_rate")] 
    int MaxHeartRate,
    
    [property: JsonPropertyName("total_elevation_gain")] 
    double? TotalElevationGain,
    
    [property: JsonPropertyName("average_seconds_per_kilometre")] 
    double? AverageSecondPerKilometre,
    
    [property: JsonPropertyName("training_effect")] 
    double? TrainingEffect
);