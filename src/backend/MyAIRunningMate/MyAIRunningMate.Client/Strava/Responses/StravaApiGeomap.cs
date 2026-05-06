using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Strava.Responses;

public class StravaApiGeomap
{
    [JsonPropertyName("summary_polyline")]
    public string SummaryPolyline { get; set; }
}