using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Interfaces.Client;

public interface IPythonApiClient
{
    Task<ActivityDto> UploadFitFileAsync(Stream fileStream, string fileName);
}