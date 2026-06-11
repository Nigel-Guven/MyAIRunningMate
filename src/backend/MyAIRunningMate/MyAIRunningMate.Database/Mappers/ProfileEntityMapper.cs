using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class ProfileEntityMapper
{
    public static Profile ToDomain(this ProfileEntity entity) =>
        new(
            userId: entity.UserId,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt
        );

    public static ProfileEntity ToEntity(this Profile domain) =>
        new()
        {
            UserId = domain.UserId,
            CreatedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt
        };
}