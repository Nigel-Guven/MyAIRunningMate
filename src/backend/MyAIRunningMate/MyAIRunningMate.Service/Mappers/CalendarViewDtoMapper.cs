using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Contracts.Calendar.Responses;

namespace MyAIRunningMate.Service.Mappers;

public static class CalendarViewDtoMapper
{
    public static CalendarViewResponse ToCalendarViewDto(this CalendarView model) => new()
    {
        ActivityId = model.ActivityId,
        StartTime = model.StartTime,
        DistanceMetres =  model.DistanceMetres ?? 0.0,
        DurationSeconds =  model.DurationSeconds,
        ExerciseType =  model.ExerciseType ?? "undefined",
        TrainingEffect =  model.TrainingEffect ?? 0.0
    };
}