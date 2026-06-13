using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.BestEfforts;

public class BestEffortService(
    IBestEffortsRepository bestEffortsRepository)
    : IBestEffortService
{
    public async Task<IEnumerable<BestEffort>> GetAllBestEfforts(Guid userId) => 
        await bestEffortsRepository.GetBestEffortsByUserId(userId);

    public async Task UpdateBestEffort(string distanceLabel, DateTime newDate, int newTime, Guid userId) => 
        await bestEffortsRepository.UpdateBestEffort(distanceLabel, newDate, newTime, userId);
}