using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Calendar;

public interface ICalendarService
{
    Task<IEnumerable<Activity>> GetMonthlyCalendarViews(DateTime byMonth, Guid userId);

}