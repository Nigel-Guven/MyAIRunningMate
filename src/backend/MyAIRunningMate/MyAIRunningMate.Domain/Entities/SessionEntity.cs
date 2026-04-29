using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Domain.Entities;

[Table("session")]
public class SessionEntity : BaseModel
{
    [PrimaryKey("user_id")]
    public Guid UserId { get; set; }
    
    [Column("athlete_id")]
    public long? AthleteId { get; set; }

    [Column("access_token")]
    public string AccessToken { get; set; }

    [Column("refresh_token")]
    public string RefreshToken { get; set; }

    [Column("expires_at")]
    public long ExpiresAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}