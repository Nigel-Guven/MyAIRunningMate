using MyAIRunningMate.Contracts.Calendar.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class CalendarDtoExtensions
{
    public static CalendarViewResponse ToCalendarViewResponse(this AggregateArtifact model) =>
        new(
            ActivityId: model.GarminActivity.ActivityId,
            ExerciseType: model.GarminActivity.ExerciseType,
            StartTime: model.GarminActivity.StartTime,
            DurationSeconds: model.GarminActivity.TotalTime,
            DistanceMetres: model.GarminActivity.DistanceMetres,
            TrainingEffectStatus: model.GarminActivityMetrics.AerobicTrainingEffect.ToString()
        );
}