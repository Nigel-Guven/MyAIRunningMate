using Microsoft.Extensions.Configuration;
using MyAIRunningMate.Domain.Interfaces.Client;
using MyAIRunningMate.Domain.Interfaces.Repositories.Session;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Mappers;
using MyAIRunningMate.Domain.Providers.StravaApi.Responses;

namespace MyAIRunningMate.Application.Strava;

public class StravaApiService : IStravaApiService
{
    private readonly IStravaApiClient _client;
    private readonly ISessionRepository _sessionRepository;
    private readonly IConfiguration _config; 

    public StravaApiService(IStravaApiClient client, ISessionRepository sessionRepository, IConfiguration config)
    {
        _client = client;
        _sessionRepository = sessionRepository;
        _config = config;
    }
    
    public string GetAuthorizationUrl(string state)
    {
        var clientId = _config["Strava:ClientId"];
        var redirectUri = _config["Strava:CallbackUrl"];
        const string scope = "read,activity:read_all";

        return $"https://www.strava.com/oauth/authorize?" +
               $"client_id={clientId}&" +
               $"response_type=code&" +
               $"redirect_uri={Uri.EscapeDataString(redirectUri!)}&" +
               $"approval_prompt=auto&" +
               $"scope={scope}&" +
               $"state={state}";
    }

    public async Task<bool> ExchangeAndSave(string code, Guid userId)
    {
        var tokenData = await _client.ExchangeCodeAsync(code);
        if (tokenData == null) return false;

        var session = tokenData.ToEntity(userId);

        await _sessionRepository.SaveSession(session);
        return true;
    }
    
    public async Task<IEnumerable<StravaApiEventResponse>> GetLatestStravaActivities(Guid userId, int amount)
    {
        var session = await _sessionRepository.GetSessionByUserId(userId);
        if (session == null) return Enumerable.Empty<StravaApiEventResponse>();

        var currentToken = session.AccessToken;
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        // Check if token is expired (60s buffer)
        if (session.ExpiresAt < (now + 60))
        {
            var newToken = await _client.RefreshTokenAsync(session.RefreshToken);
            if (newToken != null)
            {
                session.AccessToken = newToken.AccessToken;
                session.RefreshToken = newToken.RefreshToken;
                session.ExpiresAt = now + newToken.ExpiresIn;
                
                await _sessionRepository.SaveSession(session);
                currentToken = newToken.AccessToken;
            }
        }

        return await _client.GetActivitiesAsync(currentToken, amount);
    }
}