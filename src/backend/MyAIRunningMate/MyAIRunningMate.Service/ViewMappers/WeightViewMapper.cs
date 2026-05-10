using MyAIRunningMate.Contracts.Views;
using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Service.ViewMappers;

public static class WeightViewMapper
{
    public static WeightViewDto ToWeightViewDto(this WeightEntity entity) => new()
    {
        WeightInPounds = entity.WeightPounds,
        CreatedAt = entity.CreatedAt
    };
}
