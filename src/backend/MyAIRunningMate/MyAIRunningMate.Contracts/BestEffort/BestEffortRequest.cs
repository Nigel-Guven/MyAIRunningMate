using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.BestEffort;

public class BestEffortRequest
{
    [JsonPropertyName("distance_label")]
    public string DistanceLabel { get; set; }
    
    [JsonPropertyName("time_seconds")]
    public int NewPersonalRecordTime { get; set; }
    
    [JsonPropertyName("achieved_at")]
    public DateTime NewPersonalRecordDate { get; set; }
}