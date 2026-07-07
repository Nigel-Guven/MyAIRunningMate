using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database.Entities;

[Table("lap")]
public class LapEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid LapId { get; set; }

    [Column("activity_id")]
    public Guid ActivityId { get; set; }

    [Column("lap_number")]
    public int LapNumber { get; set; }
    
    [Column("lap_start_time")]
    public DateTime LapStartTime { get; set; }

    [Column("distance_metres")]
    public double DistanceMetres { get; set; }
    
    [Column("duration_seconds")]
    public double DurationSeconds { get; set; }
    
    [Column("avg_heart_rate")]
    public int AverageHeartRate { get; set; }
    
    [Column("max_heart_rate")]
    public int MaxHeartRate { get; set; }
    
    [Column("avg_speed")]
    public double AverageSpeed { get; set; }
    
    [Column("avg_cadence")]
    public int? AverageCadence { get; set; }
    
    [Column("primary_stroke")]
    public string? PrimaryStroke { get; set; }
    
    [Column("number_of_lengths")]
    public int? NumberOfLengths { get; set; }
}