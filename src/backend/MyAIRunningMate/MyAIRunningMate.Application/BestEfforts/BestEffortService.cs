using Microsoft.Extensions.Logging;
using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.BestEfforts;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.BestEfforts;

public class BestEffortService : IBestEffortService
{
    private readonly IBestEffortsRepository _bestEffortsRepository;
    private readonly ILogger<BestEffortService> _logger;

    public BestEffortService(IBestEffortsRepository bestEffortsRepository,
        ILogger<BestEffortService> logger)
    {
        _bestEffortsRepository = bestEffortsRepository;
        _logger = logger;
    }
    
    public async Task<IEnumerable<BestEffortEntity>> GetAllBestEfforts(Guid userId)
    {
        return await _bestEffortsRepository.GetBestEffortsByUserId(userId);
    }

    public async Task UpdateBestEffort(string distanceLabel, DateTime newDate, int newTime, Guid userId)
    {
        if(!BestEffortFields.ValidDistances.Contains(distanceLabel))
        {
            _logger.LogError(
                "Invalid distance label '{DistanceLabel}' for user {UserId}",
                distanceLabel,
                userId);

            return;
        }
        
        if (newTime <= 0)
        {
            _logger.LogError(
                "Invalid best effort time '{NewTime}' for user {UserId}",
                newTime,
                userId);

            return;
        }
        
        if (newDate > DateTime.UtcNow)
        {
            _logger.LogError(
                "Best effort date cannot be in the future. Date: {NewDate}, UserId: {UserId}",
                newDate,
                userId);

            return;
        }
        
        await _bestEffortsRepository.UpdateBestEffort(distanceLabel, newDate, newTime, userId);
    }
}