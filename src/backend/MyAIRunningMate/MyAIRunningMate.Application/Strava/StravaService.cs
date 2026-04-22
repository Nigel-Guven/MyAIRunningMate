using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using MyAIRunningMate.Domain.Interfaces;
using MyAIRunningMate.Domain.Interfaces.Infrastructure;

namespace MyAIRunningMate.Service;

public class StravaService : IStravaService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;
    private readonly ISessionRepository _sessionRepository;

    public StravaService(ISessionRepository sessionRepository, IConfiguration config)
    {
        _sessionRepository = sessionRepository;
        _config = config;
    }

    public string GetAuthorizationUrl() 
    {
        var clientId = _config["Strava:ClientId"];
        // This must match the 'Authorization Callback Domain' in Strava Settings
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
        
        var response = await client.PostAsync("https://www.strava.com/oauth/token", requestData);
        
        if (response.IsSuccessStatusCode)
        {
            var tokenData = await response.Content.ReadFromJsonAsync<StravaTokenResponse>();
        
            // Save tokenData to your SessionRepository (Supabase)
            // await _sessionRepository.UpsertSession(userId, tokenData);
            return true;
        }

        return false;
    }

    public async Task SyncActivities(Guid userId)
    {
        
    }
}