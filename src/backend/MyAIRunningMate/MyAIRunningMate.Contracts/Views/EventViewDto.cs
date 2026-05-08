using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Views;

public class EventViewDto
{
    [JsonPropertyName("name")]
    public string EventName { get; set; }

    [JsonPropertyName("event_date")]
    public DateTime EventDate { get; set; }

    [JsonPropertyName("location")]
    public string EventLocation { get; set; }
    
    [JsonPropertyName("distance_metres")]
    public int DistanceMetres { get; set; }

    [JsonPropertyName("event_type")]
    public string EventType { get; set; }
    
    [JsonPropertyName("event_url")]
    public string? EventUrl { get; set; }
    
    [JsonPropertyName("event_info")]
    public string? EventInfo { get; set; }
}