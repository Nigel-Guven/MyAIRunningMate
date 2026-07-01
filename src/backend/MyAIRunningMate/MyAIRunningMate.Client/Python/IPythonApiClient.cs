using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Client.Python;

public interface IPythonApiClient
{
    Task<(Activity Activity, IEnumerable<Lap> Laps)> UploadFitFileAsync(Stream fileStream, string fileName, Guid userId);

    Task<(TrainingPlan TrainingPlan, IEnumerable<TrainingPlanEvent> Events)> GenerateTrainingPlanAsync(
        string primaryGoal,
        int runningExperienceYears,
        string runningLevel,
        int trainingPlanLength,
        string poolSize,
        double userWeight,
        IEnumerable<Activity> activityHistory,
        Guid userId);
}