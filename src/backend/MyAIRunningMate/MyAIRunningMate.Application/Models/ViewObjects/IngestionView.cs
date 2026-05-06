namespace MyAIRunningMate.Application.Models.ViewObjects;

public class IngestionView
{
    public string GarminActivityId { get; set; }
    public DateTime StartTime { get; set; }
    public string ExerciseType { get; set; }
    public double DurationSeconds { get; set; }
    public double DistanceMetres { get; set; }
    public double TrainingEffect { get; set; }
}