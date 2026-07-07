using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database.Entities;

[Table("best_effort")]
public class BestEffortEntity : BaseModel
{
    [PrimaryKey("id")]
    public Guid BestEffortId { get; set; }
    
    [Column("activity_id")]
    public Guid ActivityId { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [Column("exercise_type")]
    public string ExerciseType { get; set; }

    [Column("distance_metres")]
    public int DistanceMetres { get; set; }

    [Column("distance_label")]
    public string DistanceLabel { get; set; }
    
    [Column("time_seconds")]
    public double? TimeAchievement { get; set; }

    [Column("is_personal_record")]
    public bool IsPersonalRecord { get; set; }
}