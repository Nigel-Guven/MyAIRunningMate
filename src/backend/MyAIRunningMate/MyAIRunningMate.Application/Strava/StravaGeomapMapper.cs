using MyAIRunningMate.Client.Strava.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Strava;

public static class StravaGeomapMapper
{
    public static StravaGeomap ToStravaGeomap(this StravaApiGeomap response) => new()
    {
        MapPolyline = response.SummaryPolyline
    };
}