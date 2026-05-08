using MyAIRunningMate.Contracts.Views;
using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Service.ViewMappers;

public static class BestEffortViewDtoMapper
{
    public static BestEffortViewDto ToBestEffortViewDto(this BestEffortEntity entity) => new()
    {
        DistanceMetres = entity.DistanceMetres,
        DistanceLabel = entity.DistanceLabel,
        TimeAchievement = entity.TimeAchievement,
        AchievementDate = entity.AchievementDate,
    };
}