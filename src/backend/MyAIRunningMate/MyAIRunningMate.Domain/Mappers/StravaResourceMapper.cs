using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models.DTO;
using MyAIRunningMate.Domain.Providers.StravaAPI.Responses;

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
    
    public static StravaResourceDto ToDto(this StravaApiEventResponse response, Guid stravaResourceId) => new()
    {
        ResourceId = stravaResourceId,
        StravaId = response.StravaId.ToString(),
        Name = response.Name,
        ElapsedTime = response.ElapsedTime,
        DistanceMetres = response.DistanceMetres,
        TotalElevationGain = response.TotalElevationGain,
        AverageCadence = response.AverageCadence,
        Type = response.Type,
        StartDate = response.StartDate,
        AchievementCount = response.AchievementCount,
        KudosCount = response.KudosCount,
        AthleteCount = response.AthleteCount,
        PersonalRecordCount = response.PersonalRecordCount,
        ElevationLow = response.ElevationLow,
        ElevationHigh = response.ElevationHigh,
    };
}