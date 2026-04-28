using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Models.DTO;

public class StravaGeomapDto
{
    [JsonPropertyName("map_id")]
    public Guid MapId { get; set; }
    
    [JsonPropertyName("summary_polyline")]
    public string MapPolyline { get; set; }
}