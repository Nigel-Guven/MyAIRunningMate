using MyAIRunningMate.Contracts.Nexus.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class NexusDtoExtensions
{
    public static NexusFinalizeResponse ToNexusFinalizeDto(this NexusFinalizeResult model)
    {
        return new NexusFinalizeResponse(
            TrainingPlanId: model.TrainingPlanId,
            Message: model.Message,
            EventsSaved: model.EventsSaved
        );
    }
}