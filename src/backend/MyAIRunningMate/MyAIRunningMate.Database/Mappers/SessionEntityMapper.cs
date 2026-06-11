using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class SessionEntityMapper
{
    public static Session ToDomain(this SessionEntity entity) =>
        new(
            userId: entity.UserId,
            athleteId: entity.AthleteId,
            accessToken: entity.AccessToken,
            refreshToken: entity.RefreshToken,
            expiresAt: entity.ExpiresAt,
            updatedAt: entity.UpdatedAt
        );

    public static SessionEntity ToEntity(this Session domain) =>
        new()
        {
            UserId = domain.UserId,
            AthleteId = domain.AthleteId,
            AccessToken = domain.AccessToken,
            RefreshToken = domain.RefreshToken,
            ExpiresAt = domain.ExpiresAt,
            UpdatedAt = domain.UpdatedAt
        };
}