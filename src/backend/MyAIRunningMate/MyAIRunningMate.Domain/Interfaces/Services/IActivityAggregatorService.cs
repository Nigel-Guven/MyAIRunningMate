using MyAIRunningMate.Domain.Activities;
using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.Models.Activities;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IActivityAggregatorService
{
    AggregateArtifactDto CreateAggregateArtifactDto(ActivityDto garminActivityDto, StravaResourceDto stravaActivityDto);
    StravaResourceDto MapStravaResourceDto(StravaResourceEntity stravaResourceEntity);
    ActivityDto MapGarminActivityDto(ActivityEntity entity);
    LapDto MapLapDto(LapEntity entity);
    MapDto MapMapResourceDto(StravaResourceMapEntity entity);

}