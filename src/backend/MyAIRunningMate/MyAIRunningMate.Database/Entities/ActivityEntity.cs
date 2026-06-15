using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database.Entities;

[Table("activity")]
public class ActivityEntity : BaseModel
{
    [PrimaryKey("id")]
    public Guid ActivityId { get; set; }
    
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("garmin_activity_id")]
    public string GarminActivityId { get; set; }

    [Column("start_time")]
    public DateTime StartTime { get; set; }

    [Column("type")]
    public string ExerciseType { get; set; }
    
    [Column("duration_seconds")]
    public double DurationSeconds { get; set; }
    
    [Column("moving_time_seconds")]
    public double MovingTimeSeconds { get; set; }
    
    [Column("distance_metres")]
    public double DistanceMetres { get; set; }
    
    [Column("calories")]
    public int Calories { get; set; }
    
    [Column("avg_heart_rate")]
    public int AverageHeartRate { get; set; }
    
    [Column("max_heart_rate")]
    public int MaxHeartRate { get; set; }
    
    [Column("total_elevation_gain")]
    public double? TotalElevationGain { get; set; }
    
    [Column("training_effect")]
    public double TrainingEffect { get; set; }
    
    [Column("raw_pace_seconds_per_metre")]
    public double? RawPaceSecondsPerMetre { get; set; }
    
    [Column("pool_length")]
    public int? PoolLength { get; set; }

    [Column("map_polyline")]
    public string? MapPolyline { get; set; }
    
    [Column("time_series_records")]
    public string TimeSeriesRecordsJson { get; set; }
}