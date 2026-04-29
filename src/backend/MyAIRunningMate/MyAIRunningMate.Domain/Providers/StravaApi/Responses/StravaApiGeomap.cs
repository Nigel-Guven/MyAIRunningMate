using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Providers.StravaApi.Responses;

public class StravaApiGeomap
{
    [JsonPropertyName("summary_polyline")]
    public string SummaryPolyline { get; set; }
}