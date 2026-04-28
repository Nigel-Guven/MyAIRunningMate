using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Providers.PythonFitApi.Responses;

public class ActivityFitResponse
{
    [JsonPropertyName("activity_id")]
    public Guid ActivityId { get; set; }
    
    [JsonPropertyName("garmin_id")]
    public string GarminActivityId { get; set; }
    
    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }
    
    [JsonPropertyName("type")]
    public string ExerciseType { get; set; }
    
    [JsonPropertyName("distance_metres")]
    public double DistanceMetres { get; set; }
    
    [JsonPropertyName("training_effect")]
    public double TrainingEffect { get; set; }
}