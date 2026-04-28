using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Providers.StravaAPI.Responses;

public class StravaAPITokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;
    
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = string.Empty;
    
    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; set; }
    
    [JsonPropertyName("athlete")]
    public StravaAPIAthlete? Athlete { get; set; }
}