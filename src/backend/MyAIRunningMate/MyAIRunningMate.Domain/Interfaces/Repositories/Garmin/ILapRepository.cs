using MyAIRunningMate.Database.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;

public interface ILapRepository : IBaseRepository<LapEntity> 
{
    Task<IEnumerable<LapEntity>> GetAllLaps();
    Task<IEnumerable<LapEntity>> GetAllLapsByActivityId(Guid activityId);
}