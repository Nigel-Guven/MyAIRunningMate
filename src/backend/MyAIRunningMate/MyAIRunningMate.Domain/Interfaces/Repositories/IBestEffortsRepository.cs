using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface IBestEffortsRepository
{
    Task<IEnumerable<BestEffort>> GetBestEffortsByUserId(Guid userId);
    Task InsertBestEfforts(IEnumerable<BestEffort> bestEfforts);
}