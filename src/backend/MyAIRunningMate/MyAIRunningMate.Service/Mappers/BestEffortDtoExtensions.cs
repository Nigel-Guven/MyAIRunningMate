using MyAIRunningMate.Contracts.BestEfforts.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class BestEffortDtoExtensions
{
    public static BestEffortResponse ToBestEffortResponse(this BestEffort model) => 
        new(
            ExerciseType: model.ExerciseType,
            DistanceMetres: model.EffortDistanceMetres,
            DistanceLabel: model.EffortDistanceLabel,
            TimeAchievement: model.TimeAchievement,
            IsPersonalRecord: model.IsPersonalRecord
        );
}