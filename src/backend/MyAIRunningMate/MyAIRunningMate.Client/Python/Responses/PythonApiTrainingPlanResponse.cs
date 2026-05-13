using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses;

public class PythonApiTrainingPlanResponse
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public DateTime EndDate { get; set; } 
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("training_plan_events")]
    public IEnumerable<PythonApiTrainingPlanEventResponse> TrainingPlanEvents { get; set; }
}