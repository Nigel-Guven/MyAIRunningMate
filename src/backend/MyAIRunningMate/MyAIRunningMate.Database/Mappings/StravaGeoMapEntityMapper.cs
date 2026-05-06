using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Database.Entities;

namespace MyAIRunningMate.Database.Mappings;

public static class StravaGeoMapEntityMapper
{
    public static StravaGeomapEntity ToStravaGeomapEntity(this StravaGeomap geomap, Guid mapId) => new()
    {
        MapId = mapId,
        MapPolyline =  geomap.MapPolyline,
    };
}