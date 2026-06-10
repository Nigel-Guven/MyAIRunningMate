using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface IWeightRepository
{
    Task<IEnumerable<Weight>> Get20LatestWeights(Guid userId);
    Task<Weight> GetLatestWeight(Guid userId);
    Task LogLatestWeight(Weight weight);
}