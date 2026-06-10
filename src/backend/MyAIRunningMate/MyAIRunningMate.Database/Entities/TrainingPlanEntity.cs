using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database.Entities;

[Table("training_plan")]
public class TrainingPlanEntity : BaseModel
{
    [PrimaryKey("id", shouldInsert: false)]
    public Guid TrainingPlanId { get; set; }

    [Column("created_at", ignoreOnInsert: true)]
    public DateTime? CreatedAt { get; set; }

    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("start_date")]
    public DateTime StartDate { get; set; }

    [Column("end_date")]
    public DateTime EndDate { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("description")]
    public string Description { get; set; } = string.Empty;
}
