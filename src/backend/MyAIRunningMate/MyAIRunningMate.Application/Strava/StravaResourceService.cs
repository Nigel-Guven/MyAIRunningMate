using MyAIRunningMate.Application.DbEntityMappings;
using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Domain.Interfaces.Repositories.Strava;

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
    
    public async Task<Guid> SaveStravaResourceAndMap(StravaResource stravaResource)
    {
        Guid? mapId = null;
        
        if (stravaResource.StravaGeomap != null)
        {
            mapId = Guid.NewGuid();
            var mapEntity = stravaResource.StravaGeomap.ToStravaGeomapEntity(mapId.Value);
        
            await _stravaResourceMapRepository.Insert(mapEntity);
        }
        
        var stravaResourceEntity = stravaResource.ToStravaResourceEntity();
        
        stravaResourceEntity.MapId = mapId; 

        var result = await _stravaResourceRepository.Insert(stravaResourceEntity);

        return result.ResourceId;
    }
}