using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Strava;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Responses;

namespace MyAIRunningMate.Application.Strava;

public class StravaService : IStravaService
{
    private readonly IConfiguration _config;
    private readonly ISessionRepository _sessionRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    public StravaService(ISessionRepository sessionRepository, IConfiguration config, IHttpClientFactory httpClientFactory)
    {
        _sessionRepository = sessionRepository;
        _config = config;
        _httpClientFactory = httpClientFactory;
    }

    public string GetAuthorizationUrl(string state)
    {
        var clientId = _config["Strava:ClientId"];
        var redirectUri = _config["Strava:CallbackUrl"];
        var scope = "read,activity:read_all";

        return $"https://www.strava.com/oauth/authorize?" +
               $"client_id={clientId}&" +
               $"response_type=code&" +
               $"redirect_uri={Uri.EscapeDataString(redirectUri!)}&" +
               $"approval_prompt=auto&" +
               $"scope={scope}&" +
               $"state={state}";
    }

    public async Task<bool> ExchangeCodeAndSaveTokens(string code, Guid userId)
    {
        var client = _httpClientFactory.CreateClient("Strava");
        
        var requestData = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("client_id", _config["Strava:ClientId"]!),
            new KeyValuePair<string, string>("client_secret", _config["Strava:ClientSecret"]!),
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("grant_type", "authorization_code")
        ]);

        var response = await client.PostAsync("https://www.strava.com/oauth/token", requestData);
        
        if (!response.IsSuccessStatusCode) 
            return false;

        var tokenData = await response.Content.ReadFromJsonAsync<StravaTokenResponse>();
        
        if (tokenData == null) return false;

        var session = new SessionEntity
        {
            UserId = userId,
            AccessToken = tokenData.AccessToken,
            RefreshToken = tokenData.RefreshToken,
            ExpiresAt = tokenData.ExpiresIn,
            UpdatedAt = DateTime.UtcNow
        };

        await _sessionRepository.SaveSession(session);
        return true;
    }
}