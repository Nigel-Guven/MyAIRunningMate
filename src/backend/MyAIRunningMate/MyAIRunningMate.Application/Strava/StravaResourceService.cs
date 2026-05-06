using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Database.DbEntityMappings;
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
        var resourceId = Guid.NewGuid();
        
        var stravaResourceEntity = stravaResource.ToStravaResourceEntity(resourceId);
        
        if (stravaResource.StravaGeomap != null)
        {
            var mapId = Guid.NewGuid();
            var stravaMapResourceEntity = stravaResource.StravaGeomap.ToStravaGeomapEntity(mapId);
            
            await _stravaResourceMapRepository.Insert(stravaMapResourceEntity);
            await _stravaResourceRepository.Insert(stravaResourceEntity);
        }
        
        await _stravaResourceRepository.Insert(stravaResourceEntity);

        return resourceId;
    }
}