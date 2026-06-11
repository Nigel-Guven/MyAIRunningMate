namespace MyAIRunningMate.Domain.Models;

public record Session
{
    public Guid UserId { get; init; }
    public long? AthleteId { get; init; }
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
    public long? ExpiresAt { get; init; }
    public DateTime UpdatedAt { get; init; }

    public Session(
        Guid userId, 
        long? athleteId, 
        string accessToken, 
        string refreshToken, 
        long? expiresAt, 
        DateTime updatedAt)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
            throw new ArgumentException("Access token cannot be null or empty.", nameof(accessToken));

        if (string.IsNullOrWhiteSpace(refreshToken))
            throw new ArgumentException("Refresh token cannot be null or empty.", nameof(refreshToken));

        if (athleteId.HasValue && athleteId.Value <= 0)
            throw new ArgumentException("Athlete ID must be a positive identifier if provided.", nameof(athleteId));

        UserId = userId;
        AthleteId = athleteId;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ExpiresAt = expiresAt;
        UpdatedAt = updatedAt;
    }
}