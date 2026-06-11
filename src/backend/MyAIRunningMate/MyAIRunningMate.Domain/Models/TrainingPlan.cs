namespace MyAIRunningMate.Domain.Models;

public record TrainingPlan
{
    public Guid TrainingPlanId { get; init; }
    public DateTime? CreatedAt { get; init; }
    public string Title { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public Guid UserId { get; init; }
    public string Description { get; init; }

    public TrainingPlan(
        Guid trainingPlanId,
        DateTime? createdAt,
        string title,
        DateTime startDate,
        DateTime endDate,
        Guid userId,
        string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Training plan title cannot be empty.", nameof(title));

        if (endDate < startDate)
            throw new ArgumentException("Plan end date cannot be earlier than the start date.", nameof(endDate));

        TrainingPlanId = trainingPlanId;
        CreatedAt = createdAt;
        Title = title;
        StartDate = startDate;
        EndDate = endDate;
        UserId = userId;
        Description = description;
    }
}