using MyAIRunningMate.Domain;

namespace MyAIRunningMate.Service;

public class StravaService : IStravaService
{
    private readonly Supabase.Client _supabase;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public StravaService(Supabase.Client supabase, IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _supabase = supabase;
        _httpClientFactory = httpClientFactory;
        _config = config;
    }

    public string GetAuthorizationUrl() 
    {
        var clientId = _config["Strava:ClientId"];
        return $"https://www.strava.com/oauth/authorize?client_id={clientId}..."; 
    }

    public async Task<bool> ExchangeCodeAndSaveTokens(string code)
    {
        // Logic for exchanging code...
        // Logic for saving to Supabase...
        return true;
    }

    public async Task SyncActivities(Guid userId) { /* Logic */ }
}