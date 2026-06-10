using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database.Entities;

[Table("weight")]
public class WeightEntity: BaseModel
{
        [PrimaryKey("id", shouldInsert: false)]
        public Guid WeightId { get; set; }

        [Column("weight_pounds")]
        public double WeightPounds { get; set; }
        
        [Column("user_id")]
        public Guid UserId { get; set; }
        
        [Column("created_at", ignoreOnInsert: true)]
        public DateTime? CreatedAt { get; set; }
}