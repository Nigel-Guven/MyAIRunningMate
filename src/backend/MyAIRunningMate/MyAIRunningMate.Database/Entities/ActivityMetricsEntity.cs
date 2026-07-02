using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database.Entities;

[Table("activity_metrics")]
public class ActivityMetricsEntity : BaseModel
{
    [PrimaryKey("id")] 
    public Guid ActivityId { get; set; }
    
    [Column("elapsed_time")] 
    public double ElapsedTime { get; init; }
    
    [Column("moving_time")] 
    public double MovingTime { get; init; }
    
    [Column("distance_metres")] 
    public double DistanceMetres { get; init; }
    
    [Column("total_cycles")] 
    public int TotalCycles { get; init; }
    
    [Column("total_calories")] 
    public int TotalCalories { get; init; }
    
    [Column("estimated_sweat_loss")] 
    public int? EstimatedSweatLoss { get; init; }
    
    [Column("avg_temperature")] 
    public int? AverageTemperature { get; init; }
    
    [Column("max_temperature")] 
    public int? MaxTemperature { get; init; }
    
    [Column("avg_heart_rate")] 
    public int AverageHeartRate { get; init; }
    
    [Column("avg_heart_rate")] 
    public int MaxHeartRate { get; init; }
    
    [Column("avg_power")] 
    public int? AveragePower { get; init; }
    
    [Column("max_power")] 
    public int? MaxPower { get; init; }
    
    [Column("avg_cadence")] 
    public int AverageCadence { get; init; }
    
    [Column("max_cadence")] 
    public int? MaxCadence { get; init; }
    
    [Column("avg_vertical_oscillation")] 
    public double? AverageVerticalOscillation { get; init; }
    
    [Column("step_length")] 
    public double? StepLength { get; init; }
    
    [Column("avg_vertical_ratio")] 
    public double? AverageVerticalRatio { get; init; }
    
    [Column("avg_stance_time")] 
    public double? AverageStanceTime { get; init; }
    
    [Column("aerobic_training_effect")] 
    public double AerobicTrainingEffect { get; init; }
    
    [Column("aerobic_training_effect")] 
    public double AnaerobicTrainingEffect { get; init; }
    
    [Column("avg_swolf")]  
    public int? AverageSwolf { get; init; }
    
    [Column("pool_length")] 
    public int? PoolLength { get; init; }
}