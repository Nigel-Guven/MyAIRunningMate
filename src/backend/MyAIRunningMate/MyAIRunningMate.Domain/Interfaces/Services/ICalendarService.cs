using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface ICalendarService
{
    Task<IEnumerable<CalendarViewDto>> GetMonthlyCalendarViews(DateTime byMonth);

}