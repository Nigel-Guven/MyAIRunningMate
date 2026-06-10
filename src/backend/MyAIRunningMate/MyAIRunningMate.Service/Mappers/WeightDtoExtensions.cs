using MyAIRunningMate.Contracts.Weight.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class WeightDtoExtensions
{
    public static WeightResponse ToResponse(this Weight model) =>
        new(
            Id: model.Id,
            WeightInPounds: model.WeightInPounds,
            CreatedAt: model.CreatedAt
        );
}
