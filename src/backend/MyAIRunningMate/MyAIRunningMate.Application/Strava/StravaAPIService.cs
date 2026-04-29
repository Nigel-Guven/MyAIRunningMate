using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Session;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Providers.StravaAPI.Responses;

namespace MyAIRunningMate.Application.Strava;

public class StravaApiService : IStravaAPIService
{
    private readonly IConfiguration _config;
    private readonly ISessionRepository _sessionRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    public StravaApiService(ISessionRepository sessionRepository, IConfiguration config, IHttpClientFactory httpClientFactory)
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
        var stravaClient = _httpClientFactory.CreateClient("Strava");
        
        var requestData = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("client_id", _config["Strava:ClientId"]!),
            new KeyValuePair<string, string>("client_secret", _config["Strava:ClientSecret"]!),
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("grant_type", "authorization_code")
        ]);

        var response = await stravaClient.PostAsync("https://www.strava.com/oauth/token", requestData);
        
        if (!response.IsSuccessStatusCode) 
            return false;

        var tokenData = await response.Content.ReadFromJsonAsync<StravaAPITokenResponse>();
        
        if (tokenData == null) return false;

        var session = new SessionEntity
        {
            UserId = userId,
            AthleteId = tokenData.Athlete.AthleteId,
            AccessToken = tokenData.AccessToken,
            RefreshToken = tokenData.RefreshToken,
            ExpiresAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + tokenData.ExpiresIn,
            UpdatedAt = DateTime.UtcNow,
        };

        await _sessionRepository.SaveSession(session);
        return true;
    }

    public async Task<IEnumerable<StravaApiEventResponse>> GetLatestStravaActivities(Guid userId, int amount)
    {
        var session = await _sessionRepository.GetSessionByUserId(userId);
        
        if (session == null) return Enumerable.Empty<StravaApiEventResponse>();
        
        // TODO: Add logic here to check session.ExpiresAt and call RefreshToken if necessary
        
        var stravaClient = _httpClientFactory.CreateClient("Strava");

        stravaClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", session.AccessToken);
        
        var response = await stravaClient.GetAsync($"https://www.strava.com/api/v3/athlete/activities?per_page={amount}");
        
        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<StravaApiEventResponse>();
        }
        
        var activities = await response.Content.ReadFromJsonAsync<IEnumerable<StravaApiEventResponse>>();
        return activities ?? Enumerable.Empty<StravaApiEventResponse>();
    }
}