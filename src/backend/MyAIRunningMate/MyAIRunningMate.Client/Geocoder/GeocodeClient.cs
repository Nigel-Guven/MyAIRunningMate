using System.Text.Json;

namespace MyAIRunningMate.Client.Geocoder;

public class GeocodeClient : IGeocodeClient
{
    private readonly HttpClient _httpClient;

    public GeocodeClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string?> GetReadableLocationAsync(double lat, double lng)
    {
        try
        {
            var relativeUrl = $"reverse?format=jsonv2&lat={lat}&lon={lng}";
            
            var response = await _httpClient.GetAsync(relativeUrl);
            if (!response.IsSuccessStatusCode) return "Unknown Location";

            var jsonString = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonString);
            
            if (!doc.RootElement.TryGetProperty("address", out var address)) 
                return "Unknown Location";
            
            var city = address.TryGetProperty("city", out var c) ? c.GetString() :
                address.TryGetProperty("town", out var t) ? t.GetString() : 
                address.TryGetProperty("suburb", out var s) ? s.GetString() : "";
                          
            var country = address.TryGetProperty("country", out var co) ? co.GetString() : "";

            return !string.IsNullOrEmpty(city) ? $"{city}, {country}" : country;
        }
        catch
        {
            return "Unknown Location";
        }
    }
}