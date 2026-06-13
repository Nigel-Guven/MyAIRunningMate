using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface ILapRepository
{
    Task<IEnumerable<Lap>> GetAllLaps();
    Task<IEnumerable<Lap>> GetAllLapsByActivityId(Guid activityId);
    Task<int> BulkInsertAsync(IEnumerable<Lap> laps,  Guid activityId);
}