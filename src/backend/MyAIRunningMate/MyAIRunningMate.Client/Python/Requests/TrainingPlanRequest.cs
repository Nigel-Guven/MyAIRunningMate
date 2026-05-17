using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Requests;

public class TrainingPlanRequest
{
    [JsonPropertyName("primary_goal")]
    public string PrimaryGoal { get; set; }
    
    [JsonPropertyName("running_experience_years")]
    public int RunningExperienceYears { get; set; }
    
    [JsonPropertyName("running_level")]
    public string RunningLevel { get; set; }
    
    [JsonPropertyName("training_plan_length")]
    public int TrainingPlanLength { get; set; }
    
    [JsonPropertyName("pool_size")]
    public string PoolSize { get; set; }
    
    [JsonPropertyName("weight_pounds")]
    public double WeightPounds { get; set; }
    
    [JsonPropertyName("history")]
    public IEnumerable<PythonApiActivity> RecentActivities { get; set; }
}