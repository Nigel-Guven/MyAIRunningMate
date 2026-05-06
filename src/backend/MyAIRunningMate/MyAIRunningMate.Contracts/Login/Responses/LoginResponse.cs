using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Providers.MyAIRunningMateApi;

public class LoginResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
    
    [JsonPropertyName("user_id")]
    public Guid UserId { get; set; }
    
    [JsonPropertyName("is_strava_connected")]
    public bool IsStravaConnected { get; set; }
}