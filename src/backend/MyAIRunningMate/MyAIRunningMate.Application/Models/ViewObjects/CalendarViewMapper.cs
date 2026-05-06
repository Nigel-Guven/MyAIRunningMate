using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Application.Models.ViewObjects;

public static class CalendarViewMapper
{
    public static CalendarView ToCalendarView(this ActivityEntity entity) => new()
    {
        ActivityId = entity.ActivityId,
        StartTime = entity.StartTime,
        ExerciseType = entity.ExerciseType,
        DurationSeconds = entity.DurationSeconds,
        DistanceMetres = entity.DistanceMetres,
        TrainingEffect = entity.TrainingEffect,
    };
}