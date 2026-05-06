using MyAIRunningMate.Client.Strava.Responses;
using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Database.DbEntityMappings;

public static class SessionEntityMapper
{
    public static SessionEntity ToSessionEntity(this StravaApiTokenResponse session, Guid userId) => new()
    {
        UserId = userId,
        AthleteId = session.Athlete?.AthleteId,
        AccessToken = session.AccessToken ?? string.Empty,
        RefreshToken = session.RefreshToken ?? string.Empty,
        ExpiresAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + session.ExpiresIn,
        UpdatedAt = DateTime.UtcNow,
    };
}