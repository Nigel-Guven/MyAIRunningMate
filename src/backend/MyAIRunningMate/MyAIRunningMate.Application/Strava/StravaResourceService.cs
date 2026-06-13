using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

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
    
    public async Task<Guid> SaveStravaResourceAndMap(StravaResource? stravaResource)
    {
        Guid? mapId = null;
        
        if (stravaResource != null)
        {
            mapId = Guid.NewGuid();
            var mapEntity = stravaResource.ToStravaGeomapEntity(mapId.Value);
        
            await _stravaResourceMapRepository.InsertAsync(mapEntity);
        }
        
        var stravaResourceEntity = stravaResource.ToStravaGeomapEntity();
        
        stravaResourceEntity.MapId = mapId; 

        var result = await _stravaResourceRepository.InsertAsync(stravaResourceEntity);

        return result.ResourceId;
    }
}