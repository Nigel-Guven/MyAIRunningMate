namespace MyAIRunningMate.Client.Geocoder;

public interface IGeocodeClient
{
    Task<string> GetReadableLocationAsync(double lat, double lng);
}