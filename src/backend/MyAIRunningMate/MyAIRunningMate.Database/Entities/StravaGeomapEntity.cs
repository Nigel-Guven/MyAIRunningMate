using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database.Entities;

[Table("strava_resource_map")]
public class StravaGeomapEntity : BaseModel
{
    [PrimaryKey("id", shouldInsert: true)]
    public Guid MapId { get; set; }
    
    [Column("summary_polyline")]
    public string MapPolyline { get; set; }
}