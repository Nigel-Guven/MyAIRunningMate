using MyAIRunningMate.Contracts.BestEfforts.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class BestEffortDtoExtensions
{
    public static BestEffortResponse ToBestEffortResponse(this BestEffort model) => 
        new(
        DistanceMetres: model.DistanceMetres,
        DistanceLabel: model.DistanceLabel,
        TimeAchievement: model.TimeAchievement,
        AchievementDate: model.AchievementDate
        );
}