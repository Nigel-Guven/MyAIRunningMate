using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Providers.StravaAPI.Responses;

public class StravaApiGeomap
{
    [JsonPropertyName("summary_polyline")]
    public string SummaryPolyline { get; set; }
}