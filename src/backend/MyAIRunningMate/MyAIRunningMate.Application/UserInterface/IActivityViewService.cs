using MyAIRunningMate.Application.Models.ViewObjects;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IActivityViewService
{
    Task<AggregateArtifactView> CreateAggregateActivity(Guid activityId);
}