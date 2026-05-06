using MyAIRunningMate.Client.Strava.Responses;

namespace MyAIRunningMate.Application.Strava;

public interface IStravaApiService
{
    string GetAuthorizationUrl(string state);
    Task<bool> ExchangeAndSave(string code, Guid userId);
    Task<IEnumerable<StravaApiEventResponse>> GetLatestStravaActivities(Guid userId, int amount);

}