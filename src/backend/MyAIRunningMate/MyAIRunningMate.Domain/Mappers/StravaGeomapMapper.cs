using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models.DTO;
using MyAIRunningMate.Domain.Providers.StravaApi.Responses;

namespace MyAIRunningMate.Domain.Mappers;

public static class StravaGeomapMapper
{
    public static StravaGeomapDto ToDto(this StravaGeomapEntity entity) => new()
    {
        MapId = entity.MapId,
        MapPolyline =  entity.MapPolyline,
    };

    public static StravaGeomapEntity ToEntity(this StravaGeomapDto dto) => new()
    {
        MapId = dto.MapId,
        MapPolyline = dto.MapPolyline,
    };
    
    public static StravaGeomapDto ToDto(this StravaApiGeomap response) => new()
    {
        MapPolyline = response.SummaryPolyline
    };
}