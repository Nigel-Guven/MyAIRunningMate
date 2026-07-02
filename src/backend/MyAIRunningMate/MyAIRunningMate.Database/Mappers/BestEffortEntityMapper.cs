using MyAIRunningMate.Database.Entities;

namespace MyAIRunningMate.Database.Mappers;

public static class BestEffortEntityMapper
{
    public static BestEffort ToDomain(this BestEffortEntity entity) =>
        new(
            bestEffortId: entity.BestEffortId,
            activityId: entity.ActivityId,
            userId: entity.UserId,
            exerciseType: entity.ExerciseType,
            effortDistanceMetres: entity.DistanceMetres,
            timeAchievement: entity.TimeAchievement,
            isPersonalRecord: entity.IsPersonalRecord
        );

    public static BestEffortEntity ToEntity(this BestEffort domain) =>
        new()
        {
            BestEffortId = domain.BestEffortId,
            ActivityId = domain.ActivityId,
            UserId = domain.UserId,
            ExerciseType = domain.ExerciseType,
            DistanceMetres = domain.EffortDistanceMetres,
            DistanceLabel = domain.EffortDistanceLabel,
            TimeAchievement = domain.TimeAchievement,
            IsPersonalRecord = domain.IsPersonalRecord
        };
}