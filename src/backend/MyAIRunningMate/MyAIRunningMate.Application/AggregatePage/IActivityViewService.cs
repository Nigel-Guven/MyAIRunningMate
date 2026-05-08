using MyAIRunningMate.Application.Models.ViewObjects;

namespace MyAIRunningMate.Application.AggregatePage;

public interface IActivityViewService
{
    Task<AggregateArtifactView> CreateAggregateActivity(Guid activityId, Guid userId);
}