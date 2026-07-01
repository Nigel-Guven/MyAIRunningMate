using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses.Ingestion;

public record PythonSportResponse(
    [property: JsonPropertyName("sport_type")] 
    string SportType,

    [property: JsonPropertyName("sport_sub_type")]
    string SportSubType,

    [property: JsonPropertyName("sport_name")]
    string SportName
);
    