using MyAIRunningMate.Client.Strava.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Strava;

public static class StravaResourceMapper
{
    public static StravaResource ToStravaResource(this StravaApiEventResponse response) => new()
    {
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