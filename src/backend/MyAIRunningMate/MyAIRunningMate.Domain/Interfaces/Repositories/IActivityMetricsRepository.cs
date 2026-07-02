using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface IActivityMetricsRepository
{
    Task InsertActivityMetrics(IEnumerable<ActivityMetrics> activityMetrics);
}