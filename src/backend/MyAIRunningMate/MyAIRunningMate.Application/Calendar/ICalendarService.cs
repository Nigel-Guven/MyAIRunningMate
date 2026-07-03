using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Calendar;

public interface ICalendarService
{
    Task<IEnumerable<AggregateArtifact>> GetMonthlyCalendarViews(DateTime byMonth, Guid userId);

}