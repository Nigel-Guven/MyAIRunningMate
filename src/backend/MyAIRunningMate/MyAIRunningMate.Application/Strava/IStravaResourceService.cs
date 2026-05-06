using MyAIRunningMate.Application.Models;

namespace MyAIRunningMate.Application.Strava;

public interface IStravaResourceService
{
    Task<Guid> SaveStravaResourceAndMap(StravaResource stravaResource);
}