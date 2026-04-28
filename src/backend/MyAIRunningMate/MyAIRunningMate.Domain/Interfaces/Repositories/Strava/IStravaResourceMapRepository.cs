using MyAIRunningMate.Domain.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.Strava;

public interface IStravaResourceMapRepository : IBaseRepository<StravaGeomapEntity> 
{
    Task<StravaGeomapEntity> GetMapById(Guid mapId);
}