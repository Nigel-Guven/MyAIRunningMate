using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Providers.PythonFitApi.Responses;

public class PythonAPIActivityResponse
{
    [JsonPropertyName("garmin_id")]
    public string GarminId { get; set; }
    
    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("duration_seconds")]
    public double DurationSeconds { get; set; }
    
    [JsonPropertyName("distance_metres")]
    public double DistanceMetres { get; set; }
    
    [JsonPropertyName("average_heart_rate")]
    public int AverageHeartRate { get; set; }
    
    [JsonPropertyName("max_heart_rate")]
    public int MaxHeartRate { get; set; }
    
    [JsonPropertyName("total_elevation_gain")]
    public double? TotalElevationGain { get; set; }
    
    [JsonPropertyName("training_effect")]
    public double TrainingEffect { get; set; }
    
    [JsonPropertyName("average_pace_seconds_per_kilometre")]
    public double AverageSecondPerKilometre { get; set; }
    
    [JsonPropertyName("laps")]
    public IEnumerable<PythonAPILap> Laps { get; set; }
}