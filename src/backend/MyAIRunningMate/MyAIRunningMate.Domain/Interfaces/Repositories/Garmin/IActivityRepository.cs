using MyAIRunningMate.Domain.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;

public interface IActivityRepository : IBaseRepository<ActivityEntity> 
{
    Task<IEnumerable<ActivityEntity>> GetAllActivities();
    Task<IEnumerable<ActivityEntity>> GetAllActivitiesByMonth(DateTime byMonth);
    Task<IEnumerable<ActivityEntity>> GetAllActivitiesByDay(DateTime day);
    Task<ActivityEntity?> ActivityExistsByGarminId(string garminId);
}