using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Models.Activities;

public class LapDto
{
    public Guid LapId { get; set; }
    
    [JsonPropertyName("lap")]
    public int LapNumber { get; set; }
    [JsonPropertyName("distance_metres")]
    public double Distance { get; set; }
    [JsonPropertyName("duration_seconds")]
    public double Duration { get; set; }
    [JsonPropertyName("average_heart_rate")]
    public int AverageHeartRate { get; set; }
}