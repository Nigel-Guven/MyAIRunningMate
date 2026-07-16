using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface IBestEffortsRepository
{
    Task<IEnumerable<BestEffort>> GetBestEffortsByUserId(Guid userId);
    Task<IEnumerable<BestEffort>> GetBestEffortsByActivityId(Guid activityId);
    Task InsertBestEfforts(IEnumerable<BestEffort> bestEfforts);
}