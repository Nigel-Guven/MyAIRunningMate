using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database.Entities;

[Table("time_series_record")]
public class TimeSeriesRecordEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }
    
    [Column("activity_id")]
    public Guid ActivityId { get; set; }
    
    [Column("time_series_blob")]
    public string TimeSeriesRecordsJson { get; set; }
}