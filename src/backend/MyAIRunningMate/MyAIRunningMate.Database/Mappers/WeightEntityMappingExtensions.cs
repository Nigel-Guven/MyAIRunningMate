using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class WeightEntityMappingExtensions
{
    public static Weight ToDomain(this WeightEntity entity) =>
        new Weight(
            id: entity.WeightId,
            weightInPounds: entity.WeightPounds,
            userId: entity.UserId,
            createdAt: entity.CreatedAt ?? DateTime.UtcNow
        );

    public static WeightEntity FromDomain(Weight domain) =>
        new()
        {
            WeightId = domain.Id,
            WeightPounds = domain.WeightInPounds,
            UserId = domain.UserId,
            CreatedAt = domain.CreatedAt
        };
}