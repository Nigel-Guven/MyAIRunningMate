using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Application.Models.ViewObjects;

public static class StravaGeomapViewMapper
{
    public static StravaGeomapView ToMapView(this StravaGeomapEntity entity) => new()
    {
        MapPolyline = entity.MapPolyline,
    };
}