using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses.Ingestion;

public record PythonTimeSeriesResponse(
    [property: JsonPropertyName("tsr_timestamp")]
    DateTime TsrTimeStamp,
    
    [property: JsonPropertyName("tsr_distance_metres")] 
    double? TsrDistanceMetres,
    
    [property: JsonPropertyName("tsr_heart_rate")] 
    int? TsrHeartRate,
    
    [property: JsonPropertyName("tsr_cadence")] 
    int? TsrCadence,
    
    [property: JsonPropertyName("tsr_power")] 
    int? TsrPower,
    
    [property: JsonPropertyName("tsr_latitude")] 
    double? TsrLatitude,
    
    [property: JsonPropertyName("tsr_longitude")] 
    double? TsrLongitude
);