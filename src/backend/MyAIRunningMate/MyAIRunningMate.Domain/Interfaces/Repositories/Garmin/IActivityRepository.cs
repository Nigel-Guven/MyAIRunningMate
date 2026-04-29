using MyAIRunningMate.Domain.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;

public interface IActivityRepository : IBaseRepository<ActivityEntity> 
{
    Task<IEnumerable<ActivityEntity>> GetAllActivitiesByMonth(DateTime byMonth);
    Task<ActivityEntity?> ActivityExistsByGarminId(string garminId);
}