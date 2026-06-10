namespace MyAIRunningMate.Contracts.Events.Responses;

public record EventViewResponse(
    string EventName,
    DateTime EventDate,
    string EventLocation,
    int DistanceMetres,
    string EventType,
    string? EventUrl,
    string? EventInfo
);