using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses.Ingestion;

public record PythonUserMetricsResponse(
    [property: JsonPropertyName("user_volumetric_oxygen_max")] 
    double UserVolumetricOxygenMax,
    
    [property: JsonPropertyName("user_max_heart_rate")] 
    int UserMaxHeartRate,
    
    [property: JsonPropertyName("user_lactate_threshold_heart_rate")] 
    int UserLactateThresholdHeartRate,
    
    [property: JsonPropertyName("user_lactate_threshold_power")] 
    int UserLactateThresholdPower,
    
    [property: JsonPropertyName("user_lactate_threshold_speed")] 
    double UserLactateThresholdSpeed,
    
    [property: JsonPropertyName("user_beginning_body_battery")] 
    int UserBeginningBodyBattery,
    
    [property: JsonPropertyName("user_beginning_body_potential")] 
    int UserBeginningBodyPotential
);