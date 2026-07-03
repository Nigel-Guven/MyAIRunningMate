using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Calendar;

public class CalendarService(IActivityRepository activityRepo, IActivityMetricsRepository activityMetricsRepo) : ICalendarService
{
    public async Task<IEnumerable<AggregateArtifact>> GetMonthlyCalendarViews(DateTime byMonth, Guid userId)
    {
        var activities = await activityRepo.GetAllActivitiesByMonth(byMonth, userId);

        var aggregates = await Task.WhenAll(
            activities.Select(async activity =>
            {
                var metrics = await activityMetricsRepo.GetActivityMetrics(activity.ActivityId);

                return new AggregateArtifact(
                    GarminActivity: activity,
                    GarminActivityMetrics: metrics,
                    [],null,null
                );
            }));

        return aggregates;
    }
}
