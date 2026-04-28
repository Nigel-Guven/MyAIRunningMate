using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Service.Responses;

public class StravaApiEventResponse
{
    [JsonPropertyName("id")]
    public long StravaId { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("elapsed_time")]
    public long ElapsedTime { get; set; }
    
    [JsonPropertyName("distance")]
    public double DistanceMetres { get; set; }
    
    [JsonPropertyName("total_elevation_gain")]
    public double TotalElevationGain { get; set; }
    
    [JsonPropertyName("average_cadence")]
    public double? AverageCadence { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }
    
    [JsonPropertyName("achievement_count")]
    public long AchievementCount { get; set; }
    
    [JsonPropertyName("kudos_count")]
    public long KudosCount { get; set; }
    
    [JsonPropertyName("athlete_count")]
    public long AthleteCount { get; set; }
    
    [JsonPropertyName("pr_count")]
    public long PersonalRecordCount { get; set; }
    
    [JsonPropertyName("elev_low")]
    public double ElevationLow { get; set; }
    
    [JsonPropertyName("elev_high")]
    public double ElevationHigh { get; set; }
    
    [JsonPropertyName("map")]
    public StravaApiMap Map { get; set; }
    
    public string? SummaryPolyline => Map?.SummaryPolyline;
}