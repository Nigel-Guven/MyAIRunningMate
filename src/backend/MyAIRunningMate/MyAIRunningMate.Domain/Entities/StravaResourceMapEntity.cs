using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Domain.Entities;

[Table("strava_resource")]
public class StravaResourceMapEntity : BaseModel
{
    [Column("id")]
    public Guid MapId { get; set; }
    
    [Column("summary_polyline")]
    public string MapPolyline { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}