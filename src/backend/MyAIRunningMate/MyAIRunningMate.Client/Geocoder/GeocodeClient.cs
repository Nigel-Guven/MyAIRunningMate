using System.Net.Http.Json;
using MyAIRunningMate.Client.Geocoder.Responses;

namespace MyAIRunningMate.Client.Geocoder;

public class GeocodeClient(HttpClient httpClient) : IGeocodeClient
{
    public async Task<string> GetReadableLocationAsync(double lat, double lng)
    {
        try
        {
            var relativeUrl = $"reverse?format=jsonv2&lat={lat}&lon={lng}";
            
            var response = await httpClient.GetAsync(relativeUrl);
            if (!response.IsSuccessStatusCode) 
                return "Unknown Location";

            var result = await response.Content.ReadFromJsonAsync<GeocodeReverseResponse>();
            
            return result?.ToReadableLocation() ?? "Unknown Location";
        }
        catch
        {
            return "Unknown Location";
        }
    }
}