using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Domain.DatabaseEntities;

[Table("training_plan_event")]
public class TrainingPlanEventEntity : BaseModel
{
    [PrimaryKey("id", shouldInsert: false)]
    public Guid TrainingPlanEventId { get; set; }

    [Column("created_at", ignoreOnInsert: true)]
    public DateTime? CreatedAt { get; set; }

    [Column("training_plan_id")]
    public Guid TrainingPlanId { get; set; }

    [Column("event_date")]
    public DateTime EventDate { get; set; }

    [Column("exercise_type")]
    public string ExerciseType { get; set; } = string.Empty;

    [Column("exercise_subtype")]
    public string ExerciseSubtype { get; set; } = string.Empty;

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("distance_metres")]
    public int DistanceMetres { get; set; }
}
