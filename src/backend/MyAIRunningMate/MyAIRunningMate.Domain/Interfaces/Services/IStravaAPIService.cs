using MyAIRunningMate.Domain.Providers.StravaAPI.Responses;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IStravaAPIService
{
    string GetAuthorizationUrl(string state);
    Task<bool> ExchangeCodeAndSaveTokens(string code, Guid userId);
    Task<IEnumerable<StravaApiEventResponse>> GetLatestStravaActivities(Guid userId, int amount);
    
}