namespace MyAIRunningMate.Domain.Responses;

public class StravaTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public long ExpiresIn { get; set; }
    
    public StravaAthlete? Athlete { get; set; }
}