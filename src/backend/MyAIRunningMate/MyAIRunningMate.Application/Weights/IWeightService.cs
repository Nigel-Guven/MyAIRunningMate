using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Weights;

public interface IWeightService
{
    Task<Weight?> GetLatestWeightAsync(Guid userId);
    Task<IEnumerable<Weight>> GetWeightHistoryAsync(Guid userId, int limit = 20);
    Task<Weight> LogWeightAsync(double poundWeight, Guid userId);
}