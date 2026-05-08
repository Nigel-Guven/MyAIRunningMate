using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Application.Strava;
using MyAIRunningMate.Application.User;

namespace MyAIRunningMate.Application.LinkProvider;

public class LinkProviderService : ILinkProviderService
{
    private readonly IStravaApiService _stravaApiService;
    private readonly IUserContext _userContext;
    private const int Amount = 5;
    
    public LinkProviderService(
        IStravaApiService stravaApiService,
        IUserContext userContext)
    {
        _stravaApiService = stravaApiService;
        _userContext = userContext;
    }
    
    public async Task<StravaResource?> FindAndLinkMatchAsync(Activity activity)
    {
        var userId = _userContext.GetUserId();
        
        var stravaActivities = await _stravaApiService.GetLatestStravaActivities(userId, Amount);
        
        var matchedStravaItem = stravaActivities.FirstOrDefault(s => 
            Math.Abs((s.StartDate - activity.StartTime).TotalMinutes) < 2 &&
            Math.Abs(s.DistanceMetres - activity.DistanceMetres) < 50);
        
        if (matchedStravaItem == null) return null;

        var stravaResource = matchedStravaItem.ToStravaResource();
                
        if (!string.IsNullOrEmpty(matchedStravaItem.Geomap?.SummaryPolyline))
        {
            stravaResource.StravaGeomap = matchedStravaItem.Geomap.ToStravaGeomap();
        }
        
        return stravaResource;
    }
}