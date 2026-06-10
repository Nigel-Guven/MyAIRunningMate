using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Contracts.Views;

namespace MyAIRunningMate.Service.Mappers;

public static class StravaGeomapDtoMapper
{
    public static StravaGeomapViewDto ToMapViewDto(this StravaGeomapView model) => new()
    {
        MapPolyline = model.MapPolyline,
    };
}