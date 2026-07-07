using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database.Entities;

[Table("activity")]
public class ActivityEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid ActivityId { get; set; }
    
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("garmin_activity_id")]
    public string GarminActivityId { get; set; }

    [Column("start_time")]
    public DateTime StartTime { get; set; }
    
    [Column("elapsed_time")] 
    public double TotalTime { get; init; }
    
    [Column("moving_time")] 
    public double MovingTime { get; init; }
    
    [Column("distance_metres")] 
    public double DistanceMetres { get; init; }
    
    [Column("beginning_body_battery")]
    public int BeginningBodyBattery { get; init; }
    
    [Column("beginning_body_potential")]
    public int BeginningBodyPotential { get; init; }
    
    [Column("ending_body_battery")]
    public int EndingBodyBattery { get; init; }
    
    [Column("ending_potential")]
    public int EndingPotential { get; init; }
    
    [Column("total_ascent")]
    public int? TotalAscent { get; init; }
    
    [Column("total_descent")]
    public int? TotalDescent { get; init; }
    
    [Column("recovery_time")]
    public int RecoveryTime { get; init; }
    
    [Column("exercise_type")]
    public string ExerciseType { get; init; }
    
    [Column("exercise_subtype")]
    public string ExerciseSubType { get; init; }
    
    [Column("exercise_name")]
    public string ExerciseName { get; init; }

    [Column("user_volumetric_oxygen_max")] 
    public double UserVolumetricOxygenMax { get; init; }

    [Column("user_max_heart_rate")]  
    public int UserMaxHeartRate { get; init; }
    
    [Column("user_lactate_threshold_heart_rate")]
    public int UserLactateThresholdHeartRate { get; init; }
    
    [Column("user_lactate_threshold_power")]
    public int UserLactateThresholdPower { get; init; }
    
    [Column("user_lactate_threshold_speed")]
    public double UserLactateThresholdSpeed { get; init; }
    
    [Column("number_of_laps")]
    public int NumberOfLaps { get; init; }
    
    [Column("location")]
    public string? Location { get; init; }
    
    [Column("map_polyline")]
    public string? MapPolyline { get; init; }
}