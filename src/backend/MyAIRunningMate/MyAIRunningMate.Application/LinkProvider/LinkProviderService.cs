using Microsoft.Extensions.Logging;
using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Application.Strava;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Client.Strava.Responses;

namespace MyAIRunningMate.Application.LinkProvider;

public class LinkProviderService : ILinkProviderService
{
    private readonly IStravaApiService _stravaApiService;
    private readonly IUserContext _userContext;
    private readonly ILogger<LinkProviderService> _logger;

    private const int SearchWindowHours = 24;
    private const double StartTimeToleranceMinutes = 15;
    private const double DistanceToleranceMetres = 100;
    private const double DurationToleranceSeconds = 120;

    public LinkProviderService(
        IStravaApiService stravaApiService,
        IUserContext userContext,
        ILogger<LinkProviderService> logger)
    {
        _stravaApiService = stravaApiService;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<StravaResource?> FindAndLinkMatchAsync(Activity activity)
    {
        var userId = _userContext.GetUserId();

        var stravaActivities = (await _stravaApiService.GetActivitiesAroundAsync(
            userId,
            activity.StartTime,
            SearchWindowHours)).ToList();

        if (stravaActivities.Count == 0)
        {
            _logger.LogWarning(
                "No Strava activities returned for user {UserId} around {StartTime}. Check Strava connection.",
                userId,
                activity.StartTime);
            return null;
        }

        var matchedStravaItem = stravaActivities.FirstOrDefault(s => IsMatch(s, activity));

        if (matchedStravaItem == null)
        {
            _logger.LogWarning(
                "No Strava match for FIT activity {GarminId} at {StartTime}, {Distance}m, {Duration}s among {CandidateCount} candidates.",
                activity.GarminActivityId,
                activity.StartTime,
                activity.DistanceMetres,
                activity.DurationSeconds,
                stravaActivities.Count);
            return null;
        }

        var stravaResource = matchedStravaItem.ToStravaResource();

        if (!string.IsNullOrEmpty(matchedStravaItem.Geomap?.SummaryPolyline))
        {
            stravaResource.StravaGeomap = matchedStravaItem.Geomap.ToStravaGeomap();
        }

        return stravaResource;
    }

    private static bool IsMatch(StravaApiEventResponse strava, Activity activity)
    {
        if (Math.Abs(strava.DistanceMetres - activity.DistanceMetres) > DistanceToleranceMetres)
        {
            return false;
        }

        if (Math.Abs(strava.ElapsedTime - activity.DurationSeconds) > DurationToleranceSeconds)
        {
            return false;
        }

        return StartsAreClose(strava, activity.StartTime);
    }

    private static bool StartsAreClose(StravaApiEventResponse strava, DateTime activityStart)
    {
        var activityUtc = ToUtc(activityStart);

        if (MinutesApart(ToUtc(strava.StartDate), activityUtc) <= StartTimeToleranceMinutes)
        {
            return true;
        }

        // FIT start_time is often local wall-clock without a timezone in JSON.
        if (activityStart.Kind == DateTimeKind.Unspecified &&
            strava.StartDateLocal != default &&
            MinutesApart(strava.StartDateLocal, activityStart) <= StartTimeToleranceMinutes)
        {
            return true;
        }

        return false;
    }

    private static double MinutesApart(DateTime a, DateTime b) =>
        Math.Abs((a - b).TotalMinutes);

    private static DateTime ToUtc(DateTime value) =>
        value.Kind switch
        {
            DateTimeKind.Utc => value,
            DateTimeKind.Local => value.ToUniversalTime(),
            _ => DateTime.SpecifyKind(value, DateTimeKind.Utc),
        };
}
