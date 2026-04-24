using MyAIRunningMate.Domain.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Infrastructure.Strava;

public interface IStravaResourceMapRepository : IBaseRepository<StravaResourceMapEntity> 
{
    Task<StravaResourceMapEntity> GetMapById(Guid mapId);
}