namespace MyAIRunningMate.Domain.Models;

public record StravaGeomap
{
    public Guid MapId { get; init; }
    public string MapPolyline { get; init; }

    public StravaGeomap(Guid mapId, string mapPolyline)
    {
        if (string.IsNullOrWhiteSpace(mapPolyline))
            throw new ArgumentException("Map polyline string cannot be empty or null.", nameof(mapPolyline));

        MapId = mapId;
        MapPolyline = mapPolyline;
    }
}