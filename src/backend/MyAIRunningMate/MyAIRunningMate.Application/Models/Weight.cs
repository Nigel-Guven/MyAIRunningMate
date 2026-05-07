using System.Text.Json.Serialization;

namespace MyAIRunningMate.Application.Models;

public class Weight
{
    [JsonPropertyName("weight_pounds")]
    public double WeightInPounds { get; set; }
}