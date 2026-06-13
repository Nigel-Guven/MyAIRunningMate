using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Calendar;

public class CalendarService(IActivityRepository activityRepo) : ICalendarService
{
    public async Task<IEnumerable<Activity>> GetMonthlyCalendarViews(DateTime byMonth, Guid userId)
    {
        var activities = await activityRepo.GetAllActivitiesByMonth(byMonth, userId);
        
        return activities;
    }
}
