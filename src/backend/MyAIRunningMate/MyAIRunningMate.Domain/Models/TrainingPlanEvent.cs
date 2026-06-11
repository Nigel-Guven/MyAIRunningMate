namespace MyAIRunningMate.Domain.Models;

public record TrainingPlanEvent
{
    public Guid TrainingPlanEventId { get; init; }
    public DateTime? CreatedAt { get; init; }
    public Guid TrainingPlanId { get; init; }
    public DateTime EventDate { get; init; }
    public string ExerciseType { get; init; }
    public string ExerciseSubtype { get; init; }
    public string Description { get; init; }
    public int DistanceMetres { get; init; }

    public TrainingPlanEvent(
        Guid trainingPlanEventId,
        DateTime? createdAt,
        Guid trainingPlanId,
        DateTime eventDate,
        string exerciseType,
        string exerciseSubtype,
        string description,
        int distanceMetres)
    {
        if (string.IsNullOrWhiteSpace(exerciseType))
            throw new ArgumentException("Exercise type cannot be null or empty (e.g., Running, Swimming).", nameof(exerciseType));

        if (distanceMetres < 0)
            throw new ArgumentException("Target distance metres cannot be negative.", nameof(distanceMetres));

        TrainingPlanEventId = trainingPlanEventId;
        CreatedAt = createdAt;
        TrainingPlanId = trainingPlanId;
        EventDate = eventDate;
        ExerciseType = exerciseType;
        ExerciseSubtype = exerciseSubtype;
        Description = description;
        DistanceMetres = distanceMetres;
    }
}