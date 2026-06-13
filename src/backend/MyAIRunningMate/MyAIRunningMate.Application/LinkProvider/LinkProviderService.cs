using MyAIRunningMate.Application.Strava;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Client.Strava;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.LinkProvider;

public class LinkProviderService(
    IStravaApiService stravaApiService,
    IUserContext userContext)
    : ILinkProviderService
{
    private const int StravaResourcesToReturn = 5;

    public async Task<StravaResource?> FindAndLinkMatchAsync(Activity activity)
    {
        var userId = userContext.GetUserId();

        var stravaActivities = (await stravaApiService.GetLatestStravaActivities(
            userId,
            StravaResourcesToReturn)).ToList();

        if (stravaActivities.Count == 0)
        {
            return null;
        }

        var matchedStravaItem = stravaActivities.FirstOrDefault(s => IsMatch(s, activity));

        if (matchedStravaItem == null)
        {
            return null;
        }

        var stravaResource = matchedStravaItem.ToDomain();

        if (!string.IsNullOrEmpty(matchedStravaItem.Geomap?.SummaryPolyline))
        {
            stravaResourceGeomap = matchedStravaItem.Geomap.ToStravaGeomap();
        }

        return stravaResource;
    }
}
