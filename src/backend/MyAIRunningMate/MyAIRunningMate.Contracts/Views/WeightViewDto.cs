using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Views;

public class WeightViewDto
{
    [JsonPropertyName("weight_pounds")]
    public double WeightInPounds { get; set; }
    
    [JsonPropertyName("user_id")]
    public Guid UserId { get; set; }
    
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }
}