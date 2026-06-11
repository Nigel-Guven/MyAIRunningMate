using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Requests;

public record PythonApiTrainingPlanRequest(
    [property: JsonPropertyName("primary_goal")] 
    string PrimaryGoal,
    
    [property: JsonPropertyName("running_experience_years")] 
    int RunningExperienceYears,
    
    [property: JsonPropertyName("running_level")] 
    string RunningLevel,
    
    [property: JsonPropertyName("training_plan_length")] 
    int TrainingPlanLength,
    
    [property: JsonPropertyName("pool_size")] 
    string PoolSize,
    
    [property: JsonPropertyName("user_weight")] 
    double UserWeight,
    
    [property: JsonPropertyName("history")] 
    IEnumerable<PythonApiActivity> RecentActivities
);