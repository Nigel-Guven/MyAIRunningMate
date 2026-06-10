namespace MyAIRunningMate.Contracts.Calendar.Responses;

public record CalendarViewResponse(
    Guid ActivityId,
    DateTime StartTime,
    string ExerciseType,
    double DurationSeconds,
    double DistanceMetres,
    double TrainingEffect
);