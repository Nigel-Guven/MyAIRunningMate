using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Domain.DatabaseEntities;

[Table("best_effort")]
public class BestEffortEntity : BaseModel
{
    [PrimaryKey("id")]
    public Guid BestEffortId { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("distance_metres")]
    public double? DistanceMetres { get; set; }

    [Column("distance_label")]
    public int DistanceLabel { get; set; }
    
    [Column("time_seconds")]
    public int TimeAchievement { get; set; }

    [Column("achieved_at")]
    public DateTime AchievementDate { get; set; }
}