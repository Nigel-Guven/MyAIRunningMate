using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Views;

public class BestEffortResponse
{
    [JsonPropertyName("distance_metres")]
    public int DistanceMetres { get; set; }
    
    [JsonPropertyName("distance_label")]
    public string DistanceLabel { get; set; }
    
    [JsonPropertyName("time_seconds")]
    public int? TimeAchievement { get; set; }
    
    [JsonPropertyName("achieved_at")]
    public DateTime? AchievementDate { get; set; }
}