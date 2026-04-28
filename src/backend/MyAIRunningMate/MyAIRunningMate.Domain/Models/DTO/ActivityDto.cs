using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Models.DTO;

public class ActivityDto
{
    [JsonPropertyName("activity_id")]
    public Guid ActivityId { get; set; }
    
    [JsonPropertyName("garmin_id")]
    public string GarminActivityId { get; set; }
    
    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }
    
    [JsonPropertyName("type")]
    public string ExerciseType { get; set; }
    
    [JsonPropertyName("duration_seconds")]
    public double DurationSeconds { get; set; }
    
    [JsonPropertyName("distance_metres")]
    public double DistanceMetres { get; set; }
    
    [JsonPropertyName("average_heart_rate")]
    public int AverageHeartRate { get; set; }
    
    [JsonPropertyName("max_heart_rate")]
    public int MaxHeartRate { get; set; }
    
    [JsonPropertyName("total_elevation_gain")]
    public double? TotalElevationGain { get; set; }
    
    [JsonPropertyName("average_pace_seconds_per_kilometre")]
    public double AverageSecondPerKilometre { get; set; }
    
    [JsonPropertyName("training_effect")]
    public double TrainingEffect { get; set; }
    
    [JsonPropertyName("strava_resource_id")]
    public Guid? StravaResourceId { get; set; }
    
    [JsonPropertyName("laps")]
    public IEnumerable<LapDto> Laps { get; set; }
}