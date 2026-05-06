

using MyAIRunningMate.Database.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;

public interface IActivityRepository : IBaseRepository<ActivityEntity> 
{
    Task<IEnumerable<ActivityEntity>> GetAllActivitiesByMonth(DateTime byMonth);
    Task<bool> ActivityExistsByGarminId(string garminId);
}