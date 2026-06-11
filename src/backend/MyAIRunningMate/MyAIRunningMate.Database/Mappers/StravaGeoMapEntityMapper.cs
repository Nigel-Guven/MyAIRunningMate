using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class StravaGeoMapEntityMapper
{
    public static StravaGeomap ToDomain(this StravaGeomapEntity entity) =>
        new(
            mapId: entity.MapId,
            mapPolyline: entity.MapPolyline
        );

    public static StravaGeomapEntity ToEntity(this StravaGeomap domain) =>
        new()
        {
            MapId = domain.MapId,
            MapPolyline = domain.MapPolyline
        };
}