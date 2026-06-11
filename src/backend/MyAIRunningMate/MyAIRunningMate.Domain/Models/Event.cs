namespace MyAIRunningMate.Domain.Models;

public record Event
{
    public Guid EventId { get; init; }
    public string EventName { get; init; }
    public DateTime EventDate { get; init; }
    public string EventLocation { get; init; }
    public int DistanceMetres { get; init; }
    public string EventType { get; init; }
    public string? EventUrl { get; init; }
    public string? EventInfo { get; init; }

    public Event(
        Guid eventId,
        string eventName,
        DateTime eventDate,
        string eventLocation,
        int distanceMetres,
        string eventType,
        string? eventUrl,
        string? eventInfo)
    {
        if (string.IsNullOrWhiteSpace(eventName))
            throw new ArgumentException("Event name cannot be empty.", nameof(eventName));

        if (string.IsNullOrWhiteSpace(eventLocation))
            throw new ArgumentException("Event location cannot be empty.", nameof(eventLocation));

        if (string.IsNullOrWhiteSpace(eventType))
            throw new ArgumentException("Event type cannot be empty (e.g., Race, Virtual).", nameof(eventType));

        if (distanceMetres <= 0)
            throw new ArgumentException("Event distance must be greater than zero.", nameof(distanceMetres));

        EventId = eventId;
        EventName = eventName;
        EventDate = eventDate;
        EventLocation = eventLocation;
        DistanceMetres = distanceMetres;
        EventType = eventType;
        EventUrl = eventUrl;
        EventInfo = eventInfo;
    }
}