using MyAIRunningMate.Domain.Models.Activities;

namespace MyAIRunningMate.Domain.Interfaces.Client;

public interface IPythonApiClient
{
    Task<ActivityDto> UploadFitFileAsync(Stream fileStream, string fileName);
}