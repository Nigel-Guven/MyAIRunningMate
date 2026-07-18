using MyAIRunningMate.Contracts.Calendar.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class CalendarDtoExtensions
{
    public static CalendarViewResponse ToCalendarViewResponse(this CalendarActivity model) =>
        new(
            ActivityId: model.ActivityId,
            ExerciseType: model.ExerciseType,
            StartTime: model.StartTime,
            DurationSeconds: model.DurationSeconds,
            DistanceMetres: model.DistanceMetres,
            AerobicTrainingEffect: model.AerobicTrainingEffect,
            AnaerobicTrainingEffect: model.AnaerobicTrainingEffect,
            TrainingEffectStatus: model.TrainingEffectStatus.ToString()
        );
}