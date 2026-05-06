using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Mappers;
using MyAIRunningMate.Domain.Models.DTO;
using MyAIRunningMate.Domain.Providers.PythonFitApi.Responses;

namespace MyAIRunningMate.Application.Strava;

public class LinkProviderService : ILinkProviderService
{
    private readonly IStravaResourceService _stravaResourceService;
    private readonly IStravaApiService _stravaApiService;
    private readonly IUserContext _userContext;
    private const int Amount = 5;
    
    public LinkProviderService(
        IStravaResourceService stravaResourceService,
        IStravaApiService stravaApiService,
        IUserContext userContext)
    {
        _stravaResourceService = stravaResourceService;
        _stravaApiService = stravaApiService;
        _userContext = userContext;
    }
    
    public async Task<Guid?> FindAndLinkMatchAsync(PythonAPIActivityResponse activityResponse)
    {
        var userId = _userContext.GetUserId();
        
        var stravaActivities = await _stravaApiService.GetLatestStravaActivities(userId, Amount);
        
        var match = stravaActivities.FirstOrDefault(s => 
            Math.Abs((s.StartDate - activityResponse.StartTime).TotalMinutes) < 2 &&
            Math.Abs(s.DistanceMetres - activityResponse.DistanceMetres) < 50);

        if (match == null) return null;
        
        Guid stravaResourceId = Guid.NewGuid();
        StravaGeomapDto? geomap = null;
                
        if (!string.IsNullOrEmpty(match.Geomap?.SummaryPolyline))
        {
            geomap = match.Geomap.ToDto();
            geomap.MapId = Guid.NewGuid();
        }

        var stravaResource = match.ToDto(stravaResourceId);

        await _stravaResourceService.SaveStravaResourceAndMaps(stravaResource, geomap);
        
        return stravaResourceId;
    }
}