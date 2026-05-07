using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Application.Weight;

public interface IWeightService
{
    Task<WeightEntity?> GetLatestWeightAsync(Guid userId);
    Task<IEnumerable<WeightEntity>> GetWeightHistoryAsync(Guid userId, int limit = 20);
    Task<WeightEntity> LogWeightAsync(double poundWeight, Guid userId);
}