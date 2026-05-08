using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Application.BestEfforts;

public interface IBestEffortService
{
    Task<IEnumerable<BestEffortEntity>> GetAllBestEfforts(Guid userId);
    Task UpdateBestEffort(string distanceLabel, DateTime newDate, int newTime, Guid userId);
}