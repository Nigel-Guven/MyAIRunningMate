using MyAIRunningMate.Contracts.Views;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IActivityViewService
{
    Task<AggregateArtifactViewDto?> CreateAggregateActivityDto(Guid activityId);
}