using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Models.DTO;

public class WeightDto
{
    [JsonPropertyName("weight_id")]
    public Guid WeightId { get; set; }
    
    [JsonPropertyName("weight_pounds")]
    public double WeightInPounds { get; set; }
    
    [JsonPropertyName("user_id")]
    public Guid UserId { get; set; }
    
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}