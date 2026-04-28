using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Domain.Entities;

[Table("weight")]
public class WeightEntity: BaseModel
{
        [PrimaryKey("id")]
        public Guid WeightId { get; set; }

        [Column("weight_pounds")]
        public double WeightPounds { get; set; }
        
        [Column("user_id")]
        public Guid UserId { get; set; }
        
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
}