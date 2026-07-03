using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface IActivityMetricsRepository
{
    Task<ActivityMetrics> GetActivityMetrics(Guid activityId);
    Task InsertActivityMetrics(ActivityMetrics activityMetrics);
}