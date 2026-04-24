using Microsoft.Extensions.Configuration;
using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Strava;

namespace MyAIRunningMate.Service;

public class StravaService : IStravaService
{
    private readonly IConfiguration _config;
    private readonly ISessionRepository _sessionRepository;

    public StravaService(ISessionRepository sessionRepository, IConfiguration config)
    {
        _sessionRepository = sessionRepository;
        _config = config;
    }

    public string GetAuthorizationUrl(string state) 
    {
        var clientId = _config["Strava:ClientId"];
        
        var redirectUri = "http://localhost:5000/api/strava/callback"; 
        var scope = "read,activity:read_all";

        return $"https://www.strava.com/oauth/authorize?" +
               $"client_id={clientId}&" +
               $"response_type=code&" +
               $"redirect_uri={Uri.EscapeDataString(redirectUri)}&" +
               $"approval_prompt=auto&" +
               $"scope={scope}";
    }

    public async Task<bool> ExchangeCodeAndSaveTokens(string code)
    {
        var client = _httpClientFactory.CreateClient("Strava");
        
        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _config["Strava:ClientId"]!),
            new KeyValuePair<string, string>("client_secret", _config["Strava:ClientSecret"]!),
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("grant_type", "authorization_code")
        });
    
        var response = await client.PostAsync("https://www.strava.com/api/v3/oauth/token", requestData);
    
        if (response.IsSuccessStatusCode)
        {
            var tokenData = await response.Content.ReadFromJsonAsync<StravaTokenResponse>();
        
            var session = new SessionEntity
            {
                UserId = userId,
                AccessToken = tokenData.access_token,
                RefreshToken = tokenData.refresh_token,
                ExpiresAt = tokenData.expires_at,
                UpdatedAt = DateTime.UtcNow
            };

            await _sessionRepository.From<SessionEntity>().Upsert(session);
            return true;
        }
        return false;
    }

    private async Task<string> GetValidAccessToken(Guid userId)
    {
        var session = await _sessionRepository.From<SessionEntity>()
            .Where(x => x.UserId == userId)
            .Single();

        // Buffer of 5 minutes to be safe
        if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 300 >= session.ExpiresAt)
        {
            // Call Strava Refresh Endpoint
            var client = _httpClientFactory.CreateClient();
            var refreshData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _config["Strava:ClientId"]!),
                new KeyValuePair<string, string>("client_secret", _config["Strava:ClientSecret"]!),
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", session.RefreshToken)
            });

            var response = await client.PostAsync("https://www.strava.com/api/v3/oauth/token", refreshData);
            var newToken = await response.Content.ReadFromJsonAsync<StravaTokenResponse>();

            session.AccessToken = newToken.access_token;
            session.RefreshToken = newToken.refresh_token;
            session.ExpiresAt = newToken.expires_at;
            await _sessionRepository.From<SessionEntity>().Upsert(session);
        
            return newToken.access_token;
        }

        return session.AccessToken;
    }
    
    private async Task<string> GetValidToken(Guid userId)
    {
        var session = await _sessionRepository.GetSessionByUserId(userId);

        // If token expires in less than 5 minutes, refresh it now
        if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 300 >= session.ExpiresAt)
        {
            return await RefreshToken(session);
        }

        return session.AccessToken;
    }

    private async Task<string> RefreshToken(SessionEntity session)
    {
        var client = _httpClientFactory.CreateClient();
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _config["Strava:ClientId"]),
            new KeyValuePair<string, string>("client_secret", _config["Strava:ClientSecret"]),
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("refresh_token", session.RefreshToken)
        });

        var response = await client.PostAsync("https://www.strava.com/oauth/token", content);
        var data = await response.Content.ReadFromJsonAsync<StravaTokenResponse>();

        session.AccessToken = data.AccessToken;
        session.RefreshToken = data.RefreshToken;
        session.ExpiresAt = data.ExpiresAt;
    
        await _sessionRepository.UpsertSession(session);
        return data.AccessToken;
    }
}