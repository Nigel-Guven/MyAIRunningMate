using MyAIRunningMate.Contracts.Views;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface ICalendarService
{
    Task<IEnumerable<CalendarViewDto>> GetMonthlyCalendarViews(DateTime byMonth);

}