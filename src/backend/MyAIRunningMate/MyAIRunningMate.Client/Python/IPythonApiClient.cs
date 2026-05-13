using MyAIRunningMate.Client.Python.Requests;
using MyAIRunningMate.Client.Python.Responses;

namespace MyAIRunningMate.Client.Python;

public interface IPythonApiClient
{
    Task<PythonApiActivityResponse> UploadFitFileAsync(Stream fileStream, string fileName);

    Task<PythonApiTrainingPlanResponse> ProcessTrainingPlanRequisites(string description, string length, string poolSize, double poundWeight, IEnumerable<PythonApiActivity> recentActivities);
}