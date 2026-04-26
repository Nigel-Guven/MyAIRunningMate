using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Domain.Entities;

[Table("strava_resource")]
public class StravaResourceEntity : BaseModel
{
    [PrimaryKey("id")]
    public Guid ResourceId { get; set; }

    [Column("strava_id")]
    public string StravaId { get; set; }

    [Column("resource_name")]
    public string Name { get; set; }

    [Column("elapsed_time")]
    public long ElapsedTime { get; set; }
    
    [Column("distance_metres")]
    public double DistanceMetres { get; set; }
    
    [Column("total_elevation_gain")]
    public double TotalElevationGain { get; set; }
    
    [Column("average_cadence")]
    public double? AverageCadence { get; set; }
    
    [Column("type")]
    public string Type { get; set; }
    
    [Column("start_date")]
    public DateTime StartDate { get; set; }
    
    [Column("achievement_count")]
    public long AchievementCount { get; set; }
    
    [Column("kudos_count")]
    public long KudosCount { get; set; }
    
    [Column("athlete_count")]
    public long AthleteCount { get; set; }
    
    [Column("pr_count")]
    public long PersonalRecordCount { get; set; }
    
    [Column("elevation_low")]
    public double ElevationLow { get; set; }
    
    [Column("elevation_high")]
    public double ElevationHigh { get; set; }
    
    [Column("map_id")]
    public Guid? MapId { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}