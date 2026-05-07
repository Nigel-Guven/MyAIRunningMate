using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Weight;

namespace MyAIRunningMate.Application.Weight;

public class WeightService : IWeightService
{
    private readonly IWeightRepository _weightRepository;

    public WeightService(IWeightRepository weightRepository)
    {
        _weightRepository = weightRepository;
    }

    public async Task<WeightEntity?> GetLatestWeightAsync(Guid userId)
    {
        var result = await _weightRepository.GetLatestWeight(userId);
        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<WeightEntity>> GetWeightHistoryAsync(Guid userId, int limit = 20)
    {
        return await _weightRepository.Get20LatestWeights(userId);
    }

    public async Task<WeightEntity> LogWeightAsync(double poundWeight, Guid userId)
    {
        if (poundWeight <= 0)
            throw new ArgumentException("Weight must be greater than 0.");

        var entity = new WeightEntity
        {
            WeightPounds = poundWeight,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _weightRepository.LogLatestWeight(entity);
        return entity;
    }
}