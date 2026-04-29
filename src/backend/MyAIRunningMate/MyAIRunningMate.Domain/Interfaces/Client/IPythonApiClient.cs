using MyAIRunningMate.Domain.Models.DTO;
using MyAIRunningMate.Domain.Providers.PythonFitApi.Responses;

namespace MyAIRunningMate.Domain.Interfaces.Client;

public interface IPythonApiClient
{
    Task<PythonAPIActivityResponse> UploadFitFileAsync(Stream fileStream, string fileName);
}