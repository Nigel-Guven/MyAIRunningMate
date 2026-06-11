using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class TrainingPlanMapper
{
    public static TrainingPlan ToDomain(this TrainingPlanEntity entity) =>
        new(
            trainingPlanId: entity.TrainingPlanId,
            createdAt: entity.CreatedAt,
            title: entity.Title,
            startDate: entity.StartDate,
            endDate: entity.EndDate,
            userId: entity.UserId,
            description: entity.Description
        );

    public static TrainingPlanEntity ToEntity(this TrainingPlan domain) =>
        new()
        {
            TrainingPlanId = domain.TrainingPlanId,
            CreatedAt = domain.CreatedAt,
            Title = domain.Title,
            StartDate = domain.StartDate,
            EndDate = domain.EndDate,
            UserId = domain.UserId,
            Description = domain.Description
        };
}