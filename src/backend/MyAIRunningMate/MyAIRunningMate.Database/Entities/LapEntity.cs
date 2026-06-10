using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database.Entities;

[Table("lap")]
public class LapEntity : BaseModel
{
    [PrimaryKey("id")]
    public Guid LapId { get; set; }

    [Column("activity_id")]
    public Guid ActivityId { get; set; }

    [Column("lap_number")]
    public int LapNumber { get; set; }

    [Column("distance_metres")]
    public double DistanceMetres { get; set; }
    
    [Column("duration_seconds")]
    public double DurationSeconds { get; set; }
    
    [Column("avg_heart_rate")]
    public int AverageHeartRate { get; set; }
}