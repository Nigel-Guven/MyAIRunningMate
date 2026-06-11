using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class BestEffortEntityMapper
{
    public static BestEffort ToDomain(this BestEffortEntity entity) =>
        new(
            bestEffortId: entity.BestEffortId,
            userId: entity.UserId,
            distanceMetres: entity.DistanceMetres,
            distanceLabel: entity.DistanceLabel,
            timeAchievement: entity.TimeAchievement,
            achievementDate: entity.AchievementDate
        );

    public static BestEffortEntity ToEntity(this BestEffort domain) =>
        new()
        {
            BestEffortId = domain.BestEffortId,
            UserId = domain.UserId,
            DistanceMetres = domain.DistanceMetres,
            DistanceLabel = domain.DistanceLabel,
            TimeAchievement = domain.TimeAchievement,
            AchievementDate = domain.AchievementDate
        };
}