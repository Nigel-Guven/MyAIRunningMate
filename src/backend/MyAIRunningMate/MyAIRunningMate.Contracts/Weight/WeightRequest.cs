using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Weight;

public class WeightRequest
{
    [JsonPropertyName("weight_pounds")]
    public double WeightInPounds { get; set; }
}