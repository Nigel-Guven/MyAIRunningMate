using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Views;

public class AggregateArtifactViewDto
{
    [JsonPropertyName("activity_id")]
    public Guid? ActivityId { get; set; }
    
    [JsonPropertyName("resource_id")]
    public Guid? ResourceId { get; set; }
    
    [JsonPropertyName("garmin_activity_id")]
    public string GarminActivityId { get; set; }
    
    [JsonPropertyName("strava_id")]
    public string? StravaId { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("exercise_type")]
    public string ExerciseType { get; set; } 
    
    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }
    
    [JsonPropertyName("elapsed_time")]
    public long? ElapsedTime { get; set; }
    
    [JsonPropertyName("average_cadence")]
    public double? AverageCadence { get; set; }
    
    [JsonPropertyName("average_second_per_kilometre")]
    public double AverageSecondPerKilometre { get; set; }
    
    [JsonPropertyName("total_elevation_gain")]
    public double? TotalElevationGain { get; set; }
    
    [JsonPropertyName("elevation_low")]
    public double? ElevationLow { get; set; }
    
    [JsonPropertyName("elevation_high")]
    public double? ElevationHigh { get; set; }
    
    [JsonPropertyName("duration_seconds")]
    public double DurationSeconds { get; set; }
    
    [JsonPropertyName("distance_metres")]
    public double DistanceMetres { get; set; }
    
    [JsonPropertyName("average_heart_rate")]
    public int? AverageHeartRate { get; set; }
    
    [JsonPropertyName("max_heart_rate")]
    public int? MaxHeartRate { get; set; }
    
    [JsonPropertyName("training_effect")]
    public double TrainingEffect { get; set; }
    
    [JsonPropertyName("achievement_count")]
    public long? AchievementCount { get; set; }
    
    [JsonPropertyName("kudos_count")]
    public long? KudosCount { get; set; }
    
    [JsonPropertyName("athlete_count")]
    public long? AthleteCount { get; set; }
    
    [JsonPropertyName("personal_record_count")]
    public long? PersonalRecordCount { get; set; }
    
    [JsonPropertyName("map")]
    public StravaGeomapViewDto? Map { get; set; }
    
    [JsonPropertyName("laps")]
    public IEnumerable<LapViewDto> Laps { get; set; }
}