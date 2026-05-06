using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Application.Models.ViewObjects;

public static class StravaResourceViewMapper
{
    public static StravaResourceView ToStravaResourceView(this StravaResourceEntity entity) => new()
    {
        ResourceId = entity.ResourceId,
        StravaId =  entity.StravaId,
        Name = entity.Name,
        ElapsedTime = entity.ElapsedTime,
        AverageCadence = entity.AverageCadence,
        AchievementCount = entity.AchievementCount,
        KudosCount = entity.KudosCount,
        AthleteCount = entity.AthleteCount,
        PersonalRecordCount = entity.PersonalRecordCount,
        ElevationLow = entity.ElevationLow,
        ElevationHigh = entity.ElevationHigh,
    };
}