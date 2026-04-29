using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Mappers;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Application.Strava;

public class LinkProviderService : ILinkProviderService
{
    private readonly IStravaResourceService _stravaResourceService;
    private readonly IStravaApiService _stravaApiService;
    private const int Amount = 5;
    
    public LinkProviderService(
        IStravaResourceService stravaResourceService,
        IStravaApiService stravaApiService)
    {
        _stravaResourceService = stravaResourceService;
        _stravaApiService = stravaApiService;
    }
    
    public async Task<Guid?> FindAndLinkMatchAsync(ActivityDto activity)
    {
        var stravaActivities = await _stravaApiService.GetLatestStravaActivities(Guid.NewGuid(), Amount);
        
        var match = stravaActivities.FirstOrDefault(s => 
            Math.Abs((s.StartDate - activity.StartTime).TotalMinutes) < 2 &&
            Math.Abs(s.DistanceMetres - activity.DistanceMetres) < 50);

        if (match == null) return null;
        
        Guid stravaResourceId = Guid.NewGuid();
        StravaGeomapDto? geomap = null;
                
        if (!string.IsNullOrEmpty(match.Geomap?.SummaryPolyline))
        {
            geomap = match.Geomap.ToDto();
        }

        var stravaResource = match.ToDto(stravaResourceId);

        await _stravaResourceService.SaveStravaResourceAndMaps(stravaResource, geomap);
        
        return stravaResourceId;
    }
}