namespace MyAIRunningMate.Application.Models.ViewObjects;

public class TrainingPlanView
{
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; } 
    public string Description { get; set; }
    public IEnumerable<TrainingPlanEventView> TrainingPlanEvents { get; set; }
}