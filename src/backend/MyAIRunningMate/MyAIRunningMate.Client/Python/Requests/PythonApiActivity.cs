using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Requests;

public class PythonApiActivity
{
    [JsonPropertyName("exercise_type")]
    public string ExerciseType { get; set; }
    
    [JsonPropertyName("start_date")]
    public DateTime StartTime { get; set; }
    
    [JsonPropertyName("duration_seconds")]
    public double DurationSeconds { get; set; }
    
    [JsonPropertyName("distance_metres")]
    public double? DistanceMetres { get; set; }
    
    [JsonPropertyName("average_heart_rate")]
    public int AverageHeartRate { get; set; }
    
    [JsonPropertyName("max_heart_rate")]
    public int MaxHeartRate { get; set; }
    
    [JsonPropertyName("total_elevation_gain")]
    public double? TotalElevationGain { get; set; }
    
    [JsonPropertyName("average_seconds_per_kilometre")]
    public double? AverageSecondPerKilometre { get; set; }
    
    [JsonPropertyName("training_effect")]
    public double? TrainingEffect { get; set; }
}