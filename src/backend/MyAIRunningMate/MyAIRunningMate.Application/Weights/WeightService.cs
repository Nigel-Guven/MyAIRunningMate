using MyAIRunningMate.Domain.Interfaces.Repositories;

namespace MyAIRunningMate.Application.Weights;

public class WeightService(IWeightRepository weightRepository) : IWeightService
{
    public async Task<Domain.Models.Weight?> GetLatestWeightAsync(Guid userId) => await weightRepository.GetLatestWeight(userId);

    public async Task<IEnumerable<Domain.Models.Weight>> GetWeightHistoryAsync(Guid userId, int limit = 20) => await weightRepository.Get20LatestWeights(userId);

    public async Task<Domain.Models.Weight> LogWeightAsync(double poundWeight, Guid userId)
    {
        var weight = new Domain.Models.Weight(
            id: Guid.NewGuid(),
            weightInPounds: poundWeight,
            userId: userId,
            createdAt: DateTime.UtcNow
        );

        await weightRepository.LogLatestWeight(weight);
        
        return weight;
    }
}