using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;

public interface IActivityRepository : IBaseRepository<ActivityEntity> 
{
    Task<IEnumerable<ActivityEntity>> GetAllActivitiesByMonth(DateTime byMonth, Guid userId);
    Task<bool> ActivityExistsByGarminId(string garminId, Guid userId);
}