using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses.TrainingPlan;

public record PythonApiTrainingPlanResponse(
    [property: JsonPropertyName("title")] 
    string Title,
    
    [property: JsonPropertyName("start_date")] 
    DateTime StartDate,
    
    [property: JsonPropertyName("end_date")] 
    DateTime EndDate, 
    
    [property: JsonPropertyName("description")] 
    string Description,
    
    [property: JsonPropertyName("training_plan_events")] 
    IEnumerable<PythonApiTrainingPlanEvent> TrainingPlanEvents
);