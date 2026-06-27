using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Aggregate;

public interface IActivityViewService
{
    Task<AggregateArtifact?> CreateAggregateActivity(Guid activityId, Guid userId);
}