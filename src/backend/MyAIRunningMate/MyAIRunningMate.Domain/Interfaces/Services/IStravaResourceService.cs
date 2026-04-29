using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IStravaResourceService
{
    Task SaveStravaResourceAndMaps(StravaResourceDto stravaResourceDto, StravaGeomapDto? mapDto);
}