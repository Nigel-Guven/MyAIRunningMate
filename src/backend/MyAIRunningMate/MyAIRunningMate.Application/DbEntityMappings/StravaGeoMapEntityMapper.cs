using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Database.DbEntityMappings;

public static class StravaGeoMapEntityMapper
{
    public static StravaGeomapEntity ToStravaGeomapEntity(this StravaGeomap geomap, Guid mapId) => new()
    {
        MapId = mapId,
        MapPolyline =  geomap.MapPolyline,
    };
}