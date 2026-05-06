using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Views;

public class StravaGeomapViewDto
{
    [JsonPropertyName("map_polyline")]
    public string MapPolyline { get; set; }
}