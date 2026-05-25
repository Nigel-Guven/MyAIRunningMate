using Microsoft.Extensions.Configuration;
using MyAIRunningMate.Application.DbEntityMappings;
using MyAIRunningMate.Client.Strava;
using MyAIRunningMate.Client.Strava.Responses;
using MyAIRunningMate.Domain.Interfaces.Repositories.Session;

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
        try
        {
            var tokenData = await _client.ExchangeCodeAsync(code);
            if (tokenData == null) 
            {
                Console.WriteLine("[ERROR] Strava returned a null token response.");
                return false;
            }

            var session = tokenData.ToSessionEntity(userId);
            await _sessionRepository.SaveSession(session);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Exception in ExchangeAndSave: {ex.Message}");
            return false;
        }
    }
    
    public async Task<IEnumerable<StravaApiEventResponse>> GetLatestStravaActivities(Guid userId, int amount)
    {
        var session = await _sessionRepository.GetSessionByUserId(userId);
        if (session == null || string.IsNullOrEmpty(session.AccessToken)) 
            return Enumerable.Empty<StravaApiEventResponse>();

        var currentToken = session.AccessToken;
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

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

        return await _client.GetActivitiesAsync(currentToken, perPage: amount);
    }

    public async Task<IEnumerable<StravaApiEventResponse>> GetActivitiesAroundAsync(
        Guid userId,
        DateTime startTime,
        int windowHours = 24)
    {
        var session = await _sessionRepository.GetSessionByUserId(userId);
        if (session == null || string.IsNullOrEmpty(session.AccessToken))
        {
            return Enumerable.Empty<StravaApiEventResponse>();
        }

        var currentToken = session.AccessToken;
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        if (session.ExpiresAt < now + 60)
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

        var startUtc = startTime.Kind switch
        {
            DateTimeKind.Utc => startTime,
            DateTimeKind.Local => startTime.ToUniversalTime(),
            _ => DateTime.SpecifyKind(startTime, DateTimeKind.Utc),
        };

        var after = new DateTimeOffset(startUtc.AddHours(-windowHours)).ToUnixTimeSeconds();
        var before = new DateTimeOffset(startUtc.AddHours(windowHours)).ToUnixTimeSeconds();

        return await _client.GetActivitiesAsync(currentToken, perPage: 50, afterUnix: after, beforeUnix: before);
    }
}