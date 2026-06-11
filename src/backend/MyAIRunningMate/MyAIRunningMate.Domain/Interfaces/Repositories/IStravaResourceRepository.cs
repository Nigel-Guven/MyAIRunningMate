using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface IStravaResourceRepository
{
    Task<StravaResource?> GetStravaResourceById(Guid stravaId);
    Task<IEnumerable<StravaResource>> GetAllStravaResourcesByIds(List<Guid> stravaId);
}