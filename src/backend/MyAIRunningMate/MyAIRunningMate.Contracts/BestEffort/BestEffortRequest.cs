using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.BestEffort;

public class BestEffortRequest
{
    [JsonPropertyName("distance_metres")]
    public int DistanceMetres { get; set; }
    
    [JsonPropertyName("time_seconds")]
    public int? NewPersonalRecordTime { get; set; }
    
    [JsonPropertyName("achieved_at")]
    public DateTime? NewPersonalRecordDate { get; set; }
}