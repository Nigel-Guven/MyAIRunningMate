using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.BestEfforts;

public interface IBestEffortService
{
    Task<IEnumerable<BestEffort>> GetAllBestEfforts(Guid userId);
}