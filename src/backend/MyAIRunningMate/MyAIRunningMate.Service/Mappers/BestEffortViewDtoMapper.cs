using MyAIRunningMate.Contracts.Views;
using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Service.Mappers;

public static class BestEffortViewDtoMapper
{
    public static BestEffortResponse ToBestEffortViewDto(this BestEffortEntity model) => new()
    {
        DistanceMetres = model.DistanceMetres,
        DistanceLabel = model.DistanceLabel,
        TimeAchievement = model.TimeAchievement,
        AchievementDate = model.AchievementDate,
    };
}