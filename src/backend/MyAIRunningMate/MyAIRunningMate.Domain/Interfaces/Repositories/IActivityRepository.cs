using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;

public interface IActivityRepository : IBaseRepository<ActivityEntity> 
{
    Task<IEnumerable<ActivityEntity>> GetAllActivitiesByMonth(DateTime byMonth, Guid userId);
    Task<IEnumerable<ActivityEntity>> GetAllActivitiesByYear(DateTime byYear, Guid userId);
    Task<IEnumerable<Guid>> GetCurrentWeekActivityIds(Guid userId);
    Task<bool> ActivityExistsByGarminId(string garminId, Guid userId);
    Task<ActivityEntity?> GetActivityByActivityId(Guid activityId, Guid userId);
    Task<List<ActivityEntity>> GetLatestActivities(Guid userId);
}