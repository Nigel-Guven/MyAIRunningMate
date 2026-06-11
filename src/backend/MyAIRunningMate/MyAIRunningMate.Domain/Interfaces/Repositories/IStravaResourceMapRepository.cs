using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface IStravaResourceMapRepository
{
    Task<StravaGeomap?> GetMapById(Guid mapId);
}