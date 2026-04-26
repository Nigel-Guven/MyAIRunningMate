using MyAIRunningMate.Domain.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Infrastructure.Strava;

public interface IStravaResourceRepository : IBaseRepository<StravaResourceEntity> 
{
    Task<StravaResourceEntity> GetStravaResourceById(Guid stravaId);
    Task<IEnumerable<StravaResourceEntity>> GetAllStravaResourcesByIds(List<Guid> stravaId);
}