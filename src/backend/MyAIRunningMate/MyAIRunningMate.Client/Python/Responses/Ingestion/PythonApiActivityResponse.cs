using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses.Ingestion;

public record PythonApiActivityResponse(
    [property: JsonPropertyName("garmin_id")] 
    string ActivityGarminId,
    
    [property: JsonPropertyName("sport")] 
    PythonSportResponse ActivitySport,
    
    [property: JsonPropertyName("activityMetrics")] 
    PythonActivityMetricsResponse ActivityMetricsResponse,
    
    [property: JsonPropertyName("userMetrics")] 
    PythonUserMetricsResponse ActivityUserMetricsResponse,
    
    [property: JsonPropertyName("sessions")] 
    IEnumerable<PythonSessionResponse> ActivitySessions,
    
    [property: JsonPropertyName("bestEfforts")] 
    IEnumerable<PythonBestEffortResponse>? ActivityBestEfforts,
    
    [property: JsonPropertyName("laps")] 
    IEnumerable<PythonApiLapResponse> ActivityLaps,
    
    [property: JsonPropertyName("time_series")] 
    IEnumerable<PythonApiTimeSeriesResponse>? ActivityTimeSeries
);