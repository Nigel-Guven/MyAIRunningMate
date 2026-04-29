using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Mappers;

public static class CalendarViewMapper
{
    public static CalendarViewDto ToDto(this ActivityEntity entity) => new()
    {
        ActivityId = entity.ActivityId,
        DistanceMetres =  entity.DistanceMetres ?? 0.0,
        StartTime = entity.StartTime,
        DurationSeconds =  entity.DurationSeconds,
        ExerciseType =  entity.ExerciseType,
        TrainingEffect =  entity.TrainingEffect ?? 0.0,
    };
}