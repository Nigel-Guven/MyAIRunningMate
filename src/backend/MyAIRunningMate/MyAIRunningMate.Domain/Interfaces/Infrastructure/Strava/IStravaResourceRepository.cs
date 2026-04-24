using MyAIRunningMate.Domain.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Infrastructure.Strava;

public interface IStravaResourceRepository : IBaseRepository<StravaResourceEntity> 
{
    Task<StravaResourceEntity> StravaResourceById(Guid stravaId);
}