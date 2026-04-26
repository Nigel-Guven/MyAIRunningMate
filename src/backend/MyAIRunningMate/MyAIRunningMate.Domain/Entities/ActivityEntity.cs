using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Domain.Entities;

[Table("activity")]
public class ActivityEntity : BaseModel
{
    [PrimaryKey("id")]
    public Guid ActivityId { get; set; }

    [Column("garmin_activity_id")]
    public string GarminActivityId { get; set; }

    [Column("start_time")]
    public DateTime StartTime { get; set; }

    [Column("type")]
    public string ExerciseType { get; set; }
    
    [Column("duration_seconds")]
    public double DurationSeconds { get; set; }
    
    [Column("distance_metres")]
    public double DistanceMetres { get; set; }
    
    [Column("avg_heart_rate")]
    public int AverageHeartRate { get; set; }
    
    [Column("max_heart_rate")]
    public int MaxHeartRate { get; set; }
    
    [Column("total_elevation_gain")]
    public double? TotalElevationGain { get; set; }
    
    [Column("avg_seconds_per_km")]
    public double AverageSecondPerKilometre { get; set; }
    
    [Column("training_effect")]
    public double TrainingEffect { get; set; }
    
    [Column("strava_resource_id")]
    public Guid? StravaResourceId { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}