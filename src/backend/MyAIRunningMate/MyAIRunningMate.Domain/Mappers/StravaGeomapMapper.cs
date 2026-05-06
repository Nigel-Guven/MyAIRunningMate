using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.Models.DTO;
using MyAIRunningMate.Domain.Providers.StravaApi.Responses;

namespace MyAIRunningMate.Domain.Mappers;

public static class StravaGeomapMapper
{
    public static StravaGeomap ToDto(this StravaGeomapEntity entity) => new()
    {
        MapId = entity.MapId,
        MapPolyline =  entity.MapPolyline,
    };

    public static StravaGeomapEntity ToEntity(this StravaGeomap dto) => new()
    {
        MapId = dto.MapId,
        MapPolyline = dto.MapPolyline,
    };
    
    public static StravaGeomap ToDto(this StravaApiGeomap response) => new()
    {
        MapPolyline = response.SummaryPolyline
    };
}