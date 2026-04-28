using MyAIRunningMate.Domain.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.Weight;

public interface IWeightRepository
{
    Task<IEnumerable<WeightEntity>> Get20LatestWeights(Guid userId);
    Task<IEnumerable<WeightEntity>> GetLatestWeight(Guid userId);
    Task LogLatestWeight(WeightEntity weight);
}