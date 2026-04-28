using MyAIRunningMate.Domain.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.Strava;

public interface IStravaResourceMapRepository : IBaseRepository<StravaGeoMapEntity> 
{
    Task<StravaGeoMapEntity> GetMapById(Guid mapId);
}