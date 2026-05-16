using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses;

public class PythonApiTrainingPlanEventResponse
{
    [JsonPropertyName("event_date")]
    public DateTime EventDate { get; set; }

    [JsonPropertyName("exercise_type")]
    public string ExerciseType { get; set; } 
    
    [JsonPropertyName("exercise_subtype")]
    public string ExerciseSubtype { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("distance_metres")]
    public int DistanceMetres { get; set; }
}