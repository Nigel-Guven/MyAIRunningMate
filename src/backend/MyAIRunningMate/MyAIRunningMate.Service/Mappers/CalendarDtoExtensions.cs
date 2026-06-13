using MyAIRunningMate.Contracts.Aggregates.Responses;
using MyAIRunningMate.Contracts.Calendar.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class CalendarDtoExtensions
{
    public static CalendarViewResponse ToCalendarViewResponse(this Activity model) =>
        new(
            ActivityId: model.ActivityId,
            ExerciseType: model.ExerciseType,
            StartTime: model.StartTime,
            DurationSeconds: model.DurationSeconds,
            DistanceMetres: model.DistanceMetres ?? 0.0,
            TrainingEffect: model.TrainingEffect ?? 0.0
        );
}