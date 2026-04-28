using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IAggregatorMapper
{
    Task<AggregateArtifactDto?> GetAggregateActivity(Guid activityId);
    AggregateArtifactDto CreateAggregateArtifactDto(ActivityDto garminActivityDto, StravaResourceDto stravaActivityDto);
    StravaResourceDto MapStravaResourceDto(StravaResourceEntity stravaResourceEntity, GeomapDto geomaps);
    ActivityDto MapGarminActivityDto(ActivityEntity entity, IEnumerable<LapDto> laps);
    LapDto MapLapDto(LapEntity entity);
    GeomapDto MapMapResourceDto(StravaGeoMapEntity entity);
    Task<IEnumerable<AggregateArtifactDto>> GetMonthlyAggregates(DateTime byMonth);

}