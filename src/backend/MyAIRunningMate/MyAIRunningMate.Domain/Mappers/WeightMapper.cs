using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Mappers;

public static class WeightMapper
{
    public static WeightDto ToDto(this WeightEntity entity) => new()
    {
        WeightInPounds = entity.WeightPounds,
        UserId = entity.UserId,
        CreatedAt = entity.CreatedAt
    };

    public static WeightEntity ToEntity(this WeightDto dto) => new()
    {
        WeightPounds = dto.WeightInPounds,
        UserId = dto.UserId,
        CreatedAt = dto.CreatedAt
    };
}