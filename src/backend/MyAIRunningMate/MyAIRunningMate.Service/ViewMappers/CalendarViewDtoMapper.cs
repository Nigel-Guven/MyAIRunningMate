using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Contracts.Views;

namespace MyAIRunningMate.Service.ViewMappers;

public static class CalendarViewDtoMapper
{
    public static CalendarViewDto ToCalendarViewDto(this CalendarView entity) => new()
    {
        ActivityId = entity.ActivityId,
        StartTime = entity.StartTime,
        DistanceMetres =  entity.DistanceMetres ?? 0.0,
        DurationSeconds =  entity.DurationSeconds,
        ExerciseType =  entity.ExerciseType ?? "undefined",
        TrainingEffect =  entity.TrainingEffect ?? 0.0
    };
}