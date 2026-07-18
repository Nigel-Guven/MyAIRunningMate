using MyAIRunningMate.Domain.ValueObjects;

namespace MyAIRunningMate.Domain.Models;

public class CalendarActivity
{
    public Guid ActivityId { get; init; }
    public DateTime StartTime { get; init; }
    public string ExerciseType { get; init; }
    public double DurationSeconds { get; init; }
    public double DistanceMetres { get; init; }
    public double AerobicTrainingEffect { get; init; }
    public double AnaerobicTrainingEffect { get; init; }
    public TrainingEffectStatus TrainingEffectStatus { get; init; }
}