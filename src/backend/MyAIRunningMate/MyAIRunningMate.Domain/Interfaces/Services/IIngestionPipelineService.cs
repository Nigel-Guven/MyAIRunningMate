using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IIngestionPipelineService
{
    Task<IEnumerable<AggregateArtifactDto>> GetMonthlyAggregates(DateTime byMonth);
    Task<AggregateArtifactDto?> GetAggregateActivity(Guid activityId);
}