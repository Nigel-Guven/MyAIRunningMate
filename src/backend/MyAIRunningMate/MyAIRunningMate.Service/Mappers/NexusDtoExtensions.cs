using MyAIRunningMate.Contracts.Nexus.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class NexusDtoExtensions
{
    public static TrainingPlanView ToDomain(this TrainingPlanViewResponse model) =>
        new(
            generatedTrainingPlan: new TrainingPlan(
                model.TrainingPlan.TrainingPlanId,
                null,
                model.TrainingPlan.Title,
                model.TrainingPlan.StartDate,
                model.TrainingPlan.EndDate,
                Guid.Empty,
                model.TrainingPlan.Description
            ),
            trainingPlanEvents: model.Events.Select(e => new TrainingPlanEvent(
                e.TrainingPlanEventId,
                null,
                model.TrainingPlan.TrainingPlanId,
                e.EventDate,
                e.ExerciseType,
                e.ExerciseSubtype,
                e.Description,
                e.DistanceMetres
            ))
        );

    public static TrainingPlanViewResponse ToDto(this TrainingPlanView model) =>
        new(
            TrainingPlan: new TrainingPlanResponse(
                model.GeneratedTrainingPlan.TrainingPlanId,
                model.GeneratedTrainingPlan.Title,
                model.GeneratedTrainingPlan.Description,
                model.GeneratedTrainingPlan.StartDate,
                model.GeneratedTrainingPlan.EndDate
            ),
            Events: model.TrainingPlanEvents.Select(e => new TrainingPlanEventResponse(
                e.TrainingPlanEventId,
                e.EventDate,
                e.ExerciseType,
                e.ExerciseSubtype,
                e.Description,
                e.DistanceMetres
            ))
        );
}