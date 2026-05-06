using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IStravaResourceService
{
    Task SaveStravaResourceAndMaps(StravaResource stravaResource, StravaGeomap? mapDto);
}