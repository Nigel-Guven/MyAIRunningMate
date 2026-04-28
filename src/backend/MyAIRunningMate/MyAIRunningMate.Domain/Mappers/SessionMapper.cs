using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Mappers;

public static class SessionMapper
{
    public static SessionDto ToDto(this SessionEntity entity) => new()
    {
        Id = entity.Id,
        UserId = entity.UserId,
        AthleteId = entity.AthleteId,
        AccessToken =  entity.AccessToken,
        RefreshToken = entity.RefreshToken,
        ExpiresAt = entity.ExpiresAt,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
    };

    public static SessionEntity ToEntity(this SessionDto dto) => new()
    {
        Id = dto.Id,
        UserId = dto.UserId,
        AthleteId = dto.AthleteId,
        AccessToken = dto.AccessToken,
        RefreshToken = dto.RefreshToken,
        ExpiresAt = dto.ExpiresAt,
        CreatedAt = dto.CreatedAt,
        UpdatedAt = dto.UpdatedAt,
    };
}