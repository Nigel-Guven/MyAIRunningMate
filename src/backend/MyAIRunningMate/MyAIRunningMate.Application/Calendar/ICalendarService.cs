using MyAIRunningMate.Application.Models.ViewObjects;

namespace MyAIRunningMate.Application.Calendar;

public interface ICalendarService
{
    Task<IEnumerable<CalendarView>> GetMonthlyCalendarViews(DateTime byMonth, Guid userId);

}