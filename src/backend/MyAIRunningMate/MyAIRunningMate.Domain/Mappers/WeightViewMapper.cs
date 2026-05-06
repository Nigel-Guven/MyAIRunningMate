using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Mappers;

public static class WeightViewMapper
{
    public static WeightEntity ToEntity(this Weight dto) => new()
    {
        WeightInPounds = dto.WeightInPounds,
        UserId = dto.UserId,
        CreatedAt = dto.CreatedAt
    };
}