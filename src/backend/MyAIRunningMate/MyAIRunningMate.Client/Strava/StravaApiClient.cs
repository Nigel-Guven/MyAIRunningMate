using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using MyAIRunningMate.Domain.Interfaces.Client;
using MyAIRunningMate.Domain.Providers.StravaAPI.Responses;

namespace MyAIRunningMate.Client.Strava;

public class StravaApiClient : IStravaApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public StravaApiClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }
    
    public async Task<StravaApiTokenResponse?> ExchangeCodeAsync(string code)
    {
        return await SendTokenRequest(new Dictionary<string, string>
        {
            ["client_id"] = _config["Strava:ClientId"]!,
            ["client_secret"] = _config["Strava:ClientSecret"]!,
            ["code"] = code,
            ["grant_type"] = "authorization_code"
        });
    }

    public async Task<StravaApiTokenResponse?> RefreshTokenAsync(string refreshToken)
    {
        return await SendTokenRequest(new Dictionary<string, string>
        {
            ["client_id"] = _config["Strava:ClientId"]!,
            ["client_secret"] = _config["Strava:ClientSecret"]!,
            ["refresh_token"] = refreshToken,
            ["grant_type"] = "refresh_token"
        });
    }

    public async Task<IEnumerable<StravaApiEventResponse>> GetActivitiesAsync(string accessToken, int amount)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v3/athlete/activities?per_page={amount}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        
        return response.IsSuccessStatusCode 
            ? await response.Content.ReadFromJsonAsync<IEnumerable<StravaApiEventResponse>>() ?? Enumerable.Empty<StravaApiEventResponse>()
            : Enumerable.Empty<StravaApiEventResponse>();
    }
    
    private async Task<StravaApiTokenResponse?> SendTokenRequest(Dictionary<string, string> paramsMap)
    {
        var content = new FormUrlEncodedContent(paramsMap);
        var response = await _httpClient.PostAsync("oauth/token", content);

        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<StravaApiTokenResponse>();
    }
}