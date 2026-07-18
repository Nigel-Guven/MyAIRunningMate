using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.ValueObjects;

namespace MyAIRunningMate.Application.Calendar;

public class CalendarService(IActivityRepository activityRepo, IActivityMetricsRepository activityMetricsRepo) : ICalendarService
{
    public async Task<IEnumerable<CalendarActivity>> GetMonthlyCalendarViews(DateTime byMonth, Guid userId)
    {
        var activities = await activityRepo.GetAllActivitiesByMonth(byMonth, userId);
        
        var aggregates = await Task.WhenAll(
            activities.Select(async activity =>
            {
                var metrics = await activityMetricsRepo.GetActivityMetrics(activity.ActivityId);

                return new CalendarActivity
                {
                    ActivityId = activity.ActivityId,
                    StartTime = activity.StartTime,
                    ExerciseType = activity.ExerciseType,
                    DurationSeconds =  activity.TotalTime,
                    DistanceMetres = activity.DistanceMetres,
                    AerobicTrainingEffect = metrics.AerobicTrainingEffect,
                    AnaerobicTrainingEffect = metrics.AnaerobicTrainingEffect,
                    TrainingEffectStatus = SetTrainingEffectStatus(metrics.AerobicTrainingEffect, metrics.AnaerobicTrainingEffect)
                };
            }));

        return aggregates.OrderBy(activity => activity.StartTime);
    }

    private static TrainingEffectStatus SetTrainingEffectStatus(double aerobicTrainingEffect, double anaerobicTrainingEffect)
    {
        if (anaerobicTrainingEffect >= 2.5)
        {
            return TrainingEffectStatus.Intensity;
        }

        return aerobicTrainingEffect switch
        {
            < 2.0 => TrainingEffectStatus.Recovery,
            < 3.0 => TrainingEffectStatus.Base,
            < 4.0 => TrainingEffectStatus.Tempo,
            _ => TrainingEffectStatus.Threshold
        };
    }
}
