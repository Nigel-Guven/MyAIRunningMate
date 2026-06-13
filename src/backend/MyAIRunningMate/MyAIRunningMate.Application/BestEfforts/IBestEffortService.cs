using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.BestEfforts;

public interface IBestEffortService
{
    Task<IEnumerable<BestEffort>> GetAllBestEfforts(Guid userId);
    Task UpdateBestEffort(string distanceLabel, DateTime newDate, int newTime, Guid userId);
}