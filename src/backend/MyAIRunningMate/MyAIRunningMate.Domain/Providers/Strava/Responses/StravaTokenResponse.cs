using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Providers.Strava.Responses;

public class StravaTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;
    
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = string.Empty;
    
    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; set; }
    
    [JsonPropertyName("athlete")]
    public StravaAthlete? Athlete { get; set; }
}