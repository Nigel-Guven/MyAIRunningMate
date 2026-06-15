using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.AggregatePage;

public interface IActivityViewService
{
    Task<AggregateArtifact?> CreateAggregateActivity(Guid activityId, Guid userId);
}