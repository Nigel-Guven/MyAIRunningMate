using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Database.Entities;

namespace MyAIRunningMate.Database.Mappings;

public static class StravaResourceEntityMapper
{
    public static StravaResourceEntity ToStravaResourceEntity(this StravaResource resource, Guid resourceId, Guid? mapId = null) => new()
    {
        ResourceId = resourceId,
        StravaId =  resource.StravaId,
        Name = resource.Name,
        ElapsedTime =  resource.ElapsedTime,
        DistanceMetres =  resource.DistanceMetres,
        TotalElevationGain =  resource.TotalElevationGain,
        AverageCadence =  resource.AverageCadence,
        Type =   resource.Type,
        StartDate = resource.StartDate,
        AchievementCount =  resource.AchievementCount,
        KudosCount =  resource.KudosCount,
        AthleteCount =  resource.AthleteCount,
        PersonalRecordCount =   resource.PersonalRecordCount,
        ElevationLow =  resource.ElevationLow,
        ElevationHigh =  resource.ElevationHigh,
        MapId = mapId,
    };
}