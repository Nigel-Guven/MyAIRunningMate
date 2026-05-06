using MyAIRunningMate.Client.Strava.Responses;

namespace MyAIRunningMate.Client.Strava;

public interface IStravaApiClient
{
    Task<StravaApiTokenResponse?> ExchangeCodeAsync(string code);
    Task<StravaApiTokenResponse?> RefreshTokenAsync(string refreshToken);
    Task<IEnumerable<StravaApiEventResponse>> GetActivitiesAsync(string accessToken, int amount);
}