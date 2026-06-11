using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class EventEntityMapper
{
    public static Event ToDomain(this EventEntity entity) =>
        new(
            eventId: entity.EventId,
            eventName: entity.EventName,
            eventDate: entity.EventDate,
            eventLocation: entity.EventLocation,
            distanceMetres: entity.DistanceMetres,
            eventType: entity.EventType,
            eventUrl: entity.EventUrl,
            eventInfo: entity.EventInfo
        );

    public static EventEntity ToEntity(this Event domain) =>
        new()
        {
            EventId = domain.EventId,
            EventName = domain.EventName,
            EventDate = domain.EventDate,
            EventLocation = domain.EventLocation,
            DistanceMetres = domain.DistanceMetres,
            EventType = domain.EventType,
            EventUrl = domain.EventUrl,
            EventInfo = domain.EventInfo
        };
}