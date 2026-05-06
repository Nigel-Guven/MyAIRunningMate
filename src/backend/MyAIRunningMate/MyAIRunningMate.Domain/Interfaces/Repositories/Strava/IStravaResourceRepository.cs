using MyAIRunningMate.Database.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.Strava;

public interface IStravaResourceRepository : IBaseRepository<StravaResourceEntity> 
{
    Task<StravaResourceEntity> GetStravaResourceById(Guid stravaId);
    Task<IEnumerable<StravaResourceEntity>> GetAllStravaResourcesByIds(List<Guid> stravaId);
}