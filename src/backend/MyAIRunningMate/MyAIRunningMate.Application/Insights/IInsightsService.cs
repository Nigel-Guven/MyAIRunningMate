using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Insights;

public interface IInsightsService
{
    Task<WeeklyInsights> GetWeeklyInsights(Guid userId, int weekOffset);
    Task<(YearlyStatistics Summary, IEnumerable<WeeklyInsights> WeeklyVolumes)> GetAnalyticsStatistics(Guid userId, int year);
}