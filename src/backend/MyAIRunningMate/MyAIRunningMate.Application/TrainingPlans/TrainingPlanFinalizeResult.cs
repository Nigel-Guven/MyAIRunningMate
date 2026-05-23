namespace MyAIRunningMate.Application.TrainingPlans;

public class TrainingPlanFinalizeResult
{
    public Guid TrainingPlanId { get; set; }

    public string Message { get; set; } = string.Empty;

    public int EventsSaved { get; set; }
}
