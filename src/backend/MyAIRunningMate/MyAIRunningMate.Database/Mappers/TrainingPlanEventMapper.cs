using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class TrainingPlanEventMapper
{
    public static TrainingPlanEvent ToDomain(this TrainingPlanEventEntity entity) =>
        new(
            trainingPlanEventId: entity.TrainingPlanEventId,
            createdAt: entity.CreatedAt,
            trainingPlanId: entity.TrainingPlanId,
            eventDate: entity.EventDate,
            exerciseType: entity.ExerciseType,
            exerciseSubtype: entity.ExerciseSubtype,
            description: entity.Description,
            distanceMetres: entity.DistanceMetres
        );

    public static TrainingPlanEventEntity ToEntity(this TrainingPlanEvent domain) =>
        new()
        {
            TrainingPlanEventId = domain.TrainingPlanEventId,
            CreatedAt = domain.CreatedAt,
            TrainingPlanId = domain.TrainingPlanId,
            EventDate = domain.EventDate,
            ExerciseType = domain.ExerciseType,
            ExerciseSubtype = domain.ExerciseSubtype,
            Description = domain.Description,
            DistanceMetres = domain.DistanceMetres
        };
}