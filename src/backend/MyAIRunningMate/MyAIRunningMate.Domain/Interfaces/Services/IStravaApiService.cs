using MyAIRunningMate.Domain.Providers.StravaApi.Responses;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IStravaApiService
{
    string GetAuthorizationUrl(string state);
    Task<bool> ExchangeAndSave(string code, Guid userId);
    Task<IEnumerable<StravaApiEventResponse>> GetLatestStravaActivities(Guid userId, int amount);

}