using MyAIRunningMate.Domain.Providers.PythonFitApi.Responses;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface ILinkProviderService
{
    Task<Guid?> FindAndLinkMatchAsync(PythonAPIActivityResponse activityResponse);
}