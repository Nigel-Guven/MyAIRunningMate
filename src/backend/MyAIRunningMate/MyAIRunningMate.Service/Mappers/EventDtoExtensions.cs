using MyAIRunningMate.Contracts.Events.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class EventDtoExtensions
{
    public static EventViewResponse ToEventViewDto(this EventModel model)
    {
        return new EventViewResponse(
            EventName: model.EventName,
            EventDate: model.EventDate,
            EventLocation: model.EventLocation,
            DistanceMetres: model.DistanceMetres,
            EventType: model.EventType,
            EventUrl: model.EventUrl,
            EventInfo: model.EventInfo
        );
    }
}