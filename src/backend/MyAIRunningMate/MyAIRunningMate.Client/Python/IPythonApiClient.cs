using MyAIRunningMate.Client.Python.Responses;

namespace MyAIRunningMate.Client.Python;

public interface IPythonApiClient
{
    Task<PythonApiActivityResponse> UploadFitFileAsync(Stream fileStream, string fileName);
}