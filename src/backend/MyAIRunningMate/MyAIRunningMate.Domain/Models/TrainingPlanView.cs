namespace MyAIRunningMate.Domain.Models;

public class TrainingPlanView
{
    public TrainingPlan GeneratedTrainingPlan { get; init; }
    public IEnumerable<TrainingPlanEvent> TrainingPlanEvents { get; init; }
    
    public TrainingPlanView(
        TrainingPlan generatedTrainingPlan, 
        IEnumerable<TrainingPlanEvent> trainingPlanEvents)
    {
        GeneratedTrainingPlan = generatedTrainingPlan;
        TrainingPlanEvents = trainingPlanEvents;
    }
}