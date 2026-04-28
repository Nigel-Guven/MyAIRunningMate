using MyAIRunningMate.Domain.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Infrastructure.Garmin;

public interface ILapRepository : IBaseRepository<LapEntity> 
{
    Task<IEnumerable<LapEntity>> GetAllLaps();
    Task<IEnumerable<LapEntity>> GetAllLapsByActivityId(Guid activityId);
}