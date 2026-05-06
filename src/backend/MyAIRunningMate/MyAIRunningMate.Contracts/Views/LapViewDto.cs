using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Views;

public class LapViewDto
{
    [JsonPropertyName("lap_number")]
    public int LapNumber { get; set; }
    
    [JsonPropertyName("distance_metres")]
    public double DistanceMetres { get; set; }
    
    [JsonPropertyName("duration_seconds")]
    public double DurationSeconds { get; set; }
    
    [JsonPropertyName("average_heart_rate")]
    public int AverageHeartRate { get; set; }
}