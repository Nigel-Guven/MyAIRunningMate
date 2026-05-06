using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Models;

public class StravaGeomap
{
    public Guid MapId { get; set; }
    public string MapPolyline { get; set; }
}