using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Contracts.Views;

namespace MyAIRunningMate.Service.ViewMappers;

public static class StravaGeomapDtoMapper
{
    public static StravaGeomapViewDto ToMapViewDto(this StravaGeomapView entity) => new()
    {
        MapPolyline = entity.MapPolyline,
    };
}