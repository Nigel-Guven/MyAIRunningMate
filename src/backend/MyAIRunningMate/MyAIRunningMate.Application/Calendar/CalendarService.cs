using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;
namespace MyAIRunningMate.Application.Calendar;

public class CalendarService : ICalendarService
{
    private readonly IActivityRepository _activityRepository;

    public CalendarService(
        IActivityRepository activityRepo)
    {
        _activityRepository = activityRepo;
    }

    public async Task<IEnumerable<CalendarView>> GetMonthlyCalendarViews(DateTime byMonth, Guid userId)
    {
        var activities = await _activityRepository.GetAllActivitiesByMonth(byMonth, userId);
    
        var calendarViews = activities.Select(a => a.ToCalendarView());

        return calendarViews;
    }
}
