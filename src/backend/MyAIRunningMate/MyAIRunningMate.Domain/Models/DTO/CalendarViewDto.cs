using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Models.DTO;

public class CalendarViewDto
{
    [JsonPropertyName("activity_id")]
    public Guid ActivityId { get; set; }
    
    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }
    
    [JsonPropertyName("type")]
    public string ExerciseType { get; set; }
    
    [JsonPropertyName("duration_seconds")]
    public double DurationSeconds { get; set; }
    
    [JsonPropertyName("distance_metres")]
    public double DistanceMetres { get; set; }
    
    [JsonPropertyName("training_effect")]
    public double TrainingEffect { get; set; }
}