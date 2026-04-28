using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IActivityViewService
{
    Task<AggregateArtifactDto?> GetAggregateActivity(Guid activityId);
}