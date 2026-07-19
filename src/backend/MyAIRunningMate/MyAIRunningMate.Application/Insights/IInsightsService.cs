using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Insights;

public interface IInsightsService
{
    Task<WeeklyInsights> GetWeeklyInsights(Guid userId, int weekOffset);
    Task<UserMetrics> GetUserMetrics(Guid userId);
    Task<(YearlyStatistics Summary, YearlyAnalytics yearlyAnalytics)> GetAnalyticsStatistics(Guid userId, DateTime year);
}