using System.Text.RegularExpressions;
using MyAIRunningMate.Client.Geocoder.Responses;

namespace MyAIRunningMate.Client.Geocoder;

public static partial class GeocodeClientMapper
{
    private static readonly Regex EdPattern = MyRegex();
    private static readonly Regex WardPattern = MyRegex1();
    private static readonly Regex MultiCommaPattern = MyRegex2();
    private static readonly Regex MultiSpacePattern = MyRegex3();

    public static string ToReadableLocation(this GeocodeReverseResponse response)
    {
        var address = response.Address;
        if (address == null) return "Unknown Location";
        
        var city = !string.IsNullOrWhiteSpace(address.City) ? address.City :
                   !string.IsNullOrWhiteSpace(address.Town) ? address.Town :
                   !string.IsNullOrWhiteSpace(address.Village) ? address.Village : "";

        var normalizedCity = city.Replace(" City", "", StringComparison.OrdinalIgnoreCase).Trim();

        string?[] potentialAreas = [
            address.Amenity,
            address.Park,
            address.Suburb,
            address.CityDistrict,
            address.Neighbourhood,
            address.Residential,
            address.StateDistrict
        ];

        var area = "";
        foreach (var val in potentialAreas)
        {
            if (string.IsNullOrWhiteSpace(val)) continue;

            var cleanedArea = EdPattern.Replace(val, "").Trim();
            cleanedArea = WardPattern.Replace(cleanedArea, "");
            cleanedArea = MultiCommaPattern.Replace(cleanedArea, ",");
            cleanedArea = MultiSpacePattern.Replace(cleanedArea, " ");
            cleanedArea = cleanedArea.Trim(',', ' ');

            var normalizedArea = cleanedArea.Replace(" City", "", StringComparison.OrdinalIgnoreCase).Trim();

            if (normalizedArea.Equals(normalizedCity, StringComparison.OrdinalIgnoreCase)) continue;
            area = cleanedArea;
            break;
        }
        
        if (!string.IsNullOrEmpty(area) && !string.IsNullOrEmpty(normalizedCity))
        {
            return $"{area}, {normalizedCity}";
        }
        
        if (!string.IsNullOrEmpty(normalizedCity))
        {
            return normalizedCity;
        }

        return !string.IsNullOrWhiteSpace(address.Country) ? address.Country : "Unknown Location";
    }

    [GeneratedRegex(@"\s[A-Z]\sED$|\sED$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-IE")]
    private static partial Regex MyRegex();
    [GeneratedRegex(@"\b(Ward|\d{4})\b", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-IE")]
    private static partial Regex MyRegex1();
    [GeneratedRegex(@"\s*,\s*,", RegexOptions.Compiled)]
    private static partial Regex MyRegex2();
    [GeneratedRegex(@"\s+", RegexOptions.Compiled)]
    private static partial Regex MyRegex3();
}