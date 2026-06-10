using MyAIRunningMate.Contracts.Events.Responses;
using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Service.Mappers;

public static class EventViewDtoMapper
{
    public static EventViewResponse ToEventViewDto(this EventEntity model) => new()
    {
        EventName =  model.EventName,
        EventDate =  model.EventDate,
        EventLocation = model.EventLocation,
        DistanceMetres =   model.DistanceMetres,
        EventType =  model.EventType,
        EventUrl =  model.EventUrl,
        EventInfo =   model.EventInfo,
    };
}