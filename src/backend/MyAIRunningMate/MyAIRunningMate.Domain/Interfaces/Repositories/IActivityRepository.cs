using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface IActivityRepository
{
    Task<IEnumerable<Activity>> GetAllActivitiesByMonth(DateTime byMonth, Guid userId);
    Task<IEnumerable<Activity>> GetAllActivitiesByYear(DateTime byYear, Guid userId);
    Task<IEnumerable<Guid>> GetCurrentWeekActivityIds(Guid userId, DateTime firstDateOfWeek, DateTime lastDateOfWeek);
    Task<bool> ActivityExistsByGarminId(string garminId, Guid userId);
    Task<Activity?> GetActivityByActivityId(Guid activityId, Guid userId);
    Task<List<Activity>> GetLatestActivities(Guid userId);
    Task<Activity> InsertAsync(Activity activity);
}