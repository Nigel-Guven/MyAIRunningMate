using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses;

public record PythonApiTimeSeries(
    [property: JsonPropertyName("timestamp")]
    DateTime TimeStamp,
    
    [property: JsonPropertyName("distance_metres")] 
    double? DistanceMetres,
    
    [property: JsonPropertyName("heart_rate")] 
    int? HeartRate,
    
    [property: JsonPropertyName("cadence")] 
    int? Cadence,
    
    [property: JsonPropertyName("latitude")] 
    double? Latitude,
    
    [property: JsonPropertyName("longitude")] 
    double? Longitude
);