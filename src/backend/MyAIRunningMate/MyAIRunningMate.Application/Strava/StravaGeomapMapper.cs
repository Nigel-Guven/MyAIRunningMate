using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Client.Strava.Responses;

namespace MyAIRunningMate.Application.Strava;

public static class StravaGeomapMapper
{
    public static StravaGeomap ToStravaGeomap(this StravaApiGeomap response) => new()
    {
        MapPolyline = response.SummaryPolyline
    };
}