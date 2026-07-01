using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses.Ingestion;

public record PythonBestEffortResponse(
    [property: JsonPropertyName("best_effort_timestamp")] 
    DateTime EffortStartingTime,
    
    [property: JsonPropertyName("best_effort_distance_metres")] 
    double EffortDistanceMetres,
    
    [property: JsonPropertyName("best_effort_time_seconds")] 
    double EffortAchievementTime,
    
    [property: JsonPropertyName("best_effort_is_personal_record")] 
    bool EffortIsPersonalRecord
);