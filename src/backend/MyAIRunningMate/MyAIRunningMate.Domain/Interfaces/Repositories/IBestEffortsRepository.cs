using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.BestEfforts;

public interface IBestEffortsRepository : IBaseRepository<BestEffortEntity>
{
    Task<IEnumerable<BestEffortEntity>> GetBestEffortsByUserId(Guid userId);
    Task UpdateBestEffort(string distanceLabel, DateTime newDate, int newTime, Guid userId);
}