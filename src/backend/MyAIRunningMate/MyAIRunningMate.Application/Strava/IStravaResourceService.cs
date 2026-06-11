using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Strava;

public interface IStravaResourceService
{
    Task<Guid> SaveStravaResourceAndMap(StravaResource stravaResource);
}