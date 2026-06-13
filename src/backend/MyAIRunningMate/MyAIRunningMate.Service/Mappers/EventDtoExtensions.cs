using MyAIRunningMate.Contracts.Events.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class EventDtoExtensions
{
    public static EventViewResponse ToEventResponse(this Event model) =>
        new(
            EventName: model.EventName,
            EventDate: model.EventDate,
            EventLocation: model.EventLocation,
            DistanceMetres: model.DistanceMetres,
            EventType: model.EventType,
            EventUrl: model.EventUrl,
            EventInfo: model.EventInfo
        );
}