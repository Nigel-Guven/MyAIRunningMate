using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Calendar;

public interface ICalendarService
{
    Task<IEnumerable<CalendarActivity>> GetMonthlyCalendarViews(DateTime byMonth, Guid userId);

}