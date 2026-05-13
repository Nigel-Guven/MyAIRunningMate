namespace MyAIRunningMate.Client.Python.Responses;

public class PythonApiTrainingPlanEventResponse
{
    public DateTime EventDate { get; set; }

    public string ExerciseType { get; set; } 
    
    public string ExerciseSubtype { get; set; }

    public string Description { get; set; }

    public int DistanceMetres { get; set; }
}