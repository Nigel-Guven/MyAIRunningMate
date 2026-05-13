using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Requests;

public class TrainingPlanRequest
{
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("training_plan_length")]
    public string TrainingPlanLength { get; set; }
    
    [JsonPropertyName("pool_size")]
    public string PoolSize { get; set; }
    
    [JsonPropertyName("weight_pounds")]
    public double WeightPounds { get; set; }
    
    [JsonPropertyName("activities")]
    public IEnumerable<PythonApiActivity> RecentActivities { get; set; }
}