using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.BestEfforts;

public class BestEffortService(
    IBestEffortsRepository bestEffortsRepository)
    : IBestEffortService
{
    public async Task<IEnumerable<BestEffort>> GetAllBestEfforts(Guid userId)
    {
        var personalRecords = await bestEffortsRepository.GetBestEffortsByUserId(userId);

        var bestEfforts = personalRecords
            .GroupBy(x => new { x.ExerciseType, x.EffortDistanceMetres })
            .Select(g => g.OrderBy(x => x.TimeAchievement).First())
            .ToList();
        
        return bestEfforts;
        
    }
}