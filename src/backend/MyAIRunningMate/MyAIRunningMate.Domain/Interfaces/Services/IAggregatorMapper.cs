using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.Models.Activities;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IAggregatorMapper
{
    Task<AggregateArtifactDto?> GetAggregateActivity(Guid activityId);
    AggregateArtifactDto CreateAggregateArtifactDto(ActivityDto garminActivityDto, StravaResourceDto stravaActivityDto);
    StravaResourceDto MapStravaResourceDto(StravaResourceEntity stravaResourceEntity, MapDto maps);
    ActivityDto MapGarminActivityDto(ActivityEntity entity, IEnumerable<LapDto> laps);
    LapDto MapLapDto(LapEntity entity);
    MapDto MapMapResourceDto(StravaResourceMapEntity entity);

}