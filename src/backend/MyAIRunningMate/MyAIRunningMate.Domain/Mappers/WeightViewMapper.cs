using MyAIRunningMate.Contracts.Views;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Mappers;

public static class WeightViewMapper
{
    public static WeightViewDto ToEntity(this Weight dto) => new()
    {
        WeightInPounds = dto.WeightInPounds,
        UserId = dto.UserId,
        CreatedAt = dto.CreatedAt
    };
}