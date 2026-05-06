using MyAIRunningMate.Contracts.Views;
using MyAIRunningMate.Database.Entities;

namespace MyAIRunningMate.Domain.Mappers;

public static class CalendarViewMapper
{
    public static CalendarViewDto ToCalendarViewDto(this ActivityEntity entity) => new()
    {
        ActivityId = entity.ActivityId,
        StartTime = entity.StartTime,
        DistanceMetres =  entity.DistanceMetres ?? 0.0,
        DurationSeconds =  entity.DurationSeconds,
        ExerciseType =  entity.ExerciseType ?? "undefined",
        TrainingEffect =  entity.TrainingEffect ?? 0.0
    };
}