using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models.DTO;
using MyAIRunningMate.Domain.Providers.StravaAPI.Responses;

namespace MyAIRunningMate.Domain.Mappers;

public static class SessionMapper
{
    public static SessionDto ToDto(this SessionEntity entity) => new()
    {
        UserId = entity.UserId,
        AthleteId = entity.AthleteId,
        AccessToken =  entity.AccessToken,
        RefreshToken = entity.RefreshToken,
        ExpiresAt = entity.ExpiresAt,
        UpdatedAt = entity.UpdatedAt,
    };

    public static SessionEntity ToEntity(this SessionDto dto) => new()
    {
        UserId = dto.UserId,
        AthleteId = dto.AthleteId,
        AccessToken = dto.AccessToken,
        RefreshToken = dto.RefreshToken,
        ExpiresAt = dto.ExpiresAt,
        UpdatedAt = dto.UpdatedAt,
    };
    
    public static SessionEntity ToEntity(this StravaApiTokenResponse response, Guid userId) => new()
    {
        UserId = userId,
        AthleteId = response.Athlete?.AthleteId,
        AccessToken = response.AccessToken ?? string.Empty,
        RefreshToken = response.RefreshToken ?? string.Empty,
        ExpiresAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + response.ExpiresIn,
        UpdatedAt = DateTime.UtcNow,
    };
}