namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface IBestEffortsRepository
{
    Task<IEnumerable<BestEffort>> GetBestEffortsByUserId(Guid userId);
    Task UpdateBestEffort(string distanceLabel, DateTime newDate, int newTime, Guid userId);
    Task InsertBestEfforts(IEnumerable<BestEffort> bestEfforts);
}