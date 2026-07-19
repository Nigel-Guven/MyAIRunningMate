using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database.Entities;

[Table("event")]
public class EventEntity : BaseModel
{
    [PrimaryKey("id")]
    public Guid EventId { get; set; }

    [Column("name")]
    public string EventName { get; set; } = null!;

    [Column("event_date")]
    public DateTime EventDate { get; set; }

    [Column("location")]
    public string EventLocation { get; set; } = null!;
    
    [Column("distance_metres")]
    public int DistanceMetres { get; set; }

    [Column("event_type")]
    public string EventType { get; set; } = null!;
    
    [Column("event_url")]
    public string? EventUrl { get; set; }
    
    [Column("event_info")]
    public string? EventInfo { get; set; }
}