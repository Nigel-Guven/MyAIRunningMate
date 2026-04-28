using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Mappers;

public static class StravaResourceMapper
{
    public static StravaResourceDto ToDto(this StravaResourceEntity entity) => new()
    {
        ResourceId =  entity.ResourceId,
        StravaId =  entity.StravaId,
        Name = entity.Name,
        ElapsedTime =  entity.ElapsedTime,
        DistanceMetres =   entity.DistanceMetres,
        TotalElevationGain =  entity.TotalElevationGain,
        AverageCadence =   entity.AverageCadence,
        Type = entity.Type,
        StartDate = entity.StartDate,
        AchievementCount =  entity.AchievementCount,
        KudosCount =  entity.KudosCount,
        AthleteCount = entity.AthleteCount,
        PersonalRecordCount =   entity.PersonalRecordCount,
        ElevationLow =  entity.ElevationLow,
        ElevationHigh =  entity.ElevationHigh,
    };

    public static StravaResourceEntity ToEntity(this StravaResourceDto dto) => new()
    {
        ResourceId = dto.ResourceId,
        StravaId = dto.StravaId,
        Name = dto.Name,
        ElapsedTime = dto.ElapsedTime,
        DistanceMetres = dto.DistanceMetres,
        TotalElevationGain = dto.TotalElevationGain,
        AverageCadence = dto.AverageCadence,
        Type = dto.Type,
        StartDate = dto.StartDate,
        AchievementCount = dto.AchievementCount,
        KudosCount = dto.KudosCount,
        AthleteCount = dto.AthleteCount,
        PersonalRecordCount = dto.PersonalRecordCount,
        ElevationLow = dto.ElevationLow,
        ElevationHigh = dto.ElevationHigh,
    };
}