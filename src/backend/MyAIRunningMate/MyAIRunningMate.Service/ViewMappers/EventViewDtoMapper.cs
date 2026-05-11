using MyAIRunningMate.Contracts.Views;
using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Service.ViewMappers;

public static class EventViewDtoMapper
{
    public static EventViewDto ToEventViewDto(this EventEntity entity) => new()
    {
        EventName =  entity.EventName,
        EventDate =  entity.EventDate,
        EventLocation = entity.EventLocation,
        DistanceMetres =   entity.DistanceMetres,
        EventType =  entity.EventType,
        EventUrl =  entity.EventUrl,
        EventInfo =   entity.EventInfo,
    };
}