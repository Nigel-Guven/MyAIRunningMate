using System.Text.Json;

namespace MyAIRunningMate.Client.Geocoder;

public class GeocodeClient : IGeocodeClient
{
    private readonly HttpClient _httpClient;

    public GeocodeClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> GetReadableLocationAsync(double lat, double lng)
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
                       address.TryGetProperty("village", out var v) ? v.GetString() : "";

            var normalizedCity = city?.Replace(" City", "", StringComparison.OrdinalIgnoreCase).Trim() ?? "";

            string[] areaKeys = { 
                "amenity", 
                "park", 
                "suburb", 
                "city_district",
                "neighbourhood", 
                "residential",
                "state_district"
            };

            var area = "";
            foreach (var key in areaKeys)
            {
                if (address.TryGetProperty(key, out var prop))
                {
                    var val = prop.GetString();
                    if (!string.IsNullOrEmpty(val))
                    {
                        var cleanedArea = System.Text.RegularExpressions.Regex.Replace(val, @"\s[A-Z]\sED$|\sED$", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Trim();
                        
                        cleanedArea = System.Text.RegularExpressions.Regex.Replace(cleanedArea, @"\b(Ward|\d{4})\b", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        
                        cleanedArea = System.Text.RegularExpressions.Regex.Replace(cleanedArea, @"\s*,\s*,", ",");
                        cleanedArea = System.Text.RegularExpressions.Regex.Replace(cleanedArea, @"\s+", " "); 
                        cleanedArea = cleanedArea.Trim(',', ' '); 

                        var normalizedArea = cleanedArea.Replace(" City", "", StringComparison.OrdinalIgnoreCase).Trim();

                        if (!normalizedArea.Equals(normalizedCity, StringComparison.OrdinalIgnoreCase))
                        {
                            area = cleanedArea; 
                            break;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(area) && !string.IsNullOrEmpty(normalizedCity))
            {
                return $"{area}, {normalizedCity}";
            }
            
            if (!string.IsNullOrEmpty(normalizedCity))
            {
                return normalizedCity;
            }

            return address.TryGetProperty("country", out var co) ? co.GetString() : "Unknown Location";
        }
        catch
        {
            return "Unknown Location";
        }
    }
}