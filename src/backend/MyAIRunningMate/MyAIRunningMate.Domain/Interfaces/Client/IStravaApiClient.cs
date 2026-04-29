using MyAIRunningMate.Domain.Providers.StravaApi.Responses;

namespace MyAIRunningMate.Domain.Interfaces.Client;

public interface IStravaApiClient
{
    Task<StravaApiTokenResponse?> ExchangeCodeAsync(string code);
    Task<StravaApiTokenResponse?> RefreshTokenAsync(string refreshToken);
    Task<IEnumerable<StravaApiEventResponse>> GetActivitiesAsync(string accessToken, int amount);
}