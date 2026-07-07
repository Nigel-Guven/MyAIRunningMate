using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses.Ingestion;

public record PythonApiActivityResponse(
    [property: JsonPropertyName("garmin_id")] 
    string ActivityGarminId,
    
    [property: JsonPropertyName("sport")] 
    PythonSportResponse ActivitySport,
    
    [property: JsonPropertyName("activity_metrics")] 
    PythonActivityMetricsResponse ActivityMetricsResponse,
    
    [property: JsonPropertyName("user_metrics")] 
    PythonUserMetricsResponse ActivityUserMetricsResponse,
    
    [property: JsonPropertyName("session")] 
    PythonSessionResponse ActivitySession,
    
    [property: JsonPropertyName("best_efforts")] 
    IEnumerable<PythonBestEffortResponse>? ActivityBestEfforts,
    
    [property: JsonPropertyName("laps")] 
    IEnumerable<PythonLapResponse> ActivityLaps,
    
    [property: JsonPropertyName("time_series")] 
    IEnumerable<PythonTimeSeriesResponse>? ActivityTimeSeries
);