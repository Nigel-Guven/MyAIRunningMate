using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class StravaResourceEntityMapper
{
    public static StravaResource ToDomain(this StravaResourceEntity entity) =>
        new(
            resourceId: entity.ResourceId,
            stravaId: entity.StravaId,
            name: entity.Name,
            elapsedTime: entity.ElapsedTime,
            distanceMetres: entity.DistanceMetres,
            totalElevationGain: entity.TotalElevationGain,
            averageCadence: entity.AverageCadence,
            type: entity.Type,
            startDate: entity.StartDate,
            achievementCount: entity.AchievementCount,
            kudosCount: entity.KudosCount,
            athleteCount: entity.AthleteCount,
            personalRecordCount: entity.PersonalRecordCount,
            elevationLow: entity.ElevationLow,
            elevationHigh: entity.ElevationHigh,
            mapId: entity.MapId
        );

    public static StravaResourceEntity ToEntity(this StravaResource domain) =>
        new()
        {
            ResourceId = domain.ResourceId,
            StravaId = domain.StravaId,
            Name = domain.Name,
            ElapsedTime = domain.ElapsedTime,
            DistanceMetres = domain.DistanceMetres,
            TotalElevationGain = domain.TotalElevationGain,
            AverageCadence = domain.AverageCadence,
            Type = domain.Type,
            StartDate = domain.StartDate,
            AchievementCount = domain.AchievementCount,
            KudosCount = domain.KudosCount,
            AthleteCount = domain.AthleteCount,
            PersonalRecordCount = domain.PersonalRecordCount,
            ElevationLow = domain.ElevationLow,
            ElevationHigh = domain.ElevationHigh,
            MapId = domain.MapId
        };
}