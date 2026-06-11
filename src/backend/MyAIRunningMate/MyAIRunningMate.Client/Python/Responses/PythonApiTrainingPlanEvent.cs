using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses;

public record PythonApiTrainingPlanEvent(
    [property: JsonPropertyName("event_date")] 
    DateTime EventDate,
    
    [property: JsonPropertyName("exercise_type")] 
    string ExerciseType, 
    
    [property: JsonPropertyName("exercise_subtype")] 
    string ExerciseSubtype,
    
    [property: JsonPropertyName("description")] 
    string Description,
    
    [property: JsonPropertyName("distance_metres")] 
    int DistanceMetres
);