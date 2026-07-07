using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses.Ingestion;

public record PythonActivityMetricsResponse(
    [property: JsonPropertyName("activity_metrics_start_time")] 
    DateTime ActivityStartTime,
    
    [property: JsonPropertyName("activity_metrics_ending_body_battery")]
    int ActivityEndingBodyBattery,
    
    [property: JsonPropertyName("activity_metrics_ending_potential")]
    int ActivityEndingPotential,
    
    [property: JsonPropertyName("activity_metrics_total_ascent")]
    int? ActivityTotalAscent,
    
    [property: JsonPropertyName("activity_metrics_total_descent")]
    int? ActivityTotalDescent,
    
    [property: JsonPropertyName("activity_metrics_recovery_time")]
    int ActivityRecoveryTime,
    
    [property: JsonPropertyName("activity_metrics_num_laps")]
    int ActivityNumLaps
);