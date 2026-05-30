using MyAIRunningMate.Application.Models;

namespace MyAIRunningMate.Application.Insights;

public interface IInsightsService
{
    Task<WeeklyInsights> GetWeeklyInsights(Guid userId);
}