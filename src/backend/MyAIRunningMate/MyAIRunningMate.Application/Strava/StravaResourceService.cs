using MyAIRunningMate.Domain.Interfaces.Repositories.Strava;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Mappers;
using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Application.Strava;

public class StravaResourceService : IStravaResourceService
{
    private readonly IStravaResourceRepository _stravaResourceRepository;
    private readonly IStravaResourceMapRepository _stravaResourceMapRepository;
    
    public StravaResourceService( 
        IStravaResourceRepository stravaResourceRepository,
        IStravaResourceMapRepository stravaResourceMapRepository)
    {
        _stravaResourceRepository = stravaResourceRepository;
        _stravaResourceMapRepository = stravaResourceMapRepository;
    }
    
    public async Task SaveStravaResourceAndMaps(StravaResource stravaResource, StravaGeomap? mapDto)
    {
        var stravaResourceEntity = stravaResource.ToEntity();

        if (mapDto != null)
        {
            stravaResourceEntity.MapId = mapDto.MapId;
            await _stravaResourceMapRepository.Insert(mapDto.ToEntity());
        }
        
        await _stravaResourceRepository.Insert(stravaResourceEntity);
    }
}