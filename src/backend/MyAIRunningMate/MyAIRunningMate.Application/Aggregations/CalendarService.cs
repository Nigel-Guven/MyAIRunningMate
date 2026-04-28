using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Mappers;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Application.Aggregations;

public class CalendarService : ICalendarService
{
    private readonly IActivityRepository _activityRepository;
    
    public CalendarService(
        IActivityRepository activityRepo)
    {
        _activityRepository = activityRepo;
    }
    
    public async Task<IEnumerable<CalendarViewDto>> GetMonthlyTiles(DateTime byMonth)
    {
        var activities = await _activityRepository.GetAllActivitiesByMonth(byMonth);
        
        var calendarViews = activities.Select(a => a.ToDto());

        return calendarViews;
    }
}