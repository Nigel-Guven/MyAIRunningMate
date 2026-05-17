namespace MyAIRunningMate.Application.Models.ViewObjects;

public abstract class TrainingPlanEventView
{
    public DateTime EventDate { get; set; }
    public string ExerciseType { get; set; } 
    public string ExerciseSubtype { get; set; }
    public string Description { get; set; }
    public int DistanceMetres { get; set; }
}