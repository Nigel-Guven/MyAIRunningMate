using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models.DTO;

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
}