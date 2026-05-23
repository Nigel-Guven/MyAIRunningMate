namespace MyAIRunningMate.Contracts.Nexus;

public class TrainingPlanFinalizeResponse
{
    public Guid TrainingPlanId { get; set; }

    public string Message { get; set; } = string.Empty;

    public int EventsSaved { get; set; }
}
