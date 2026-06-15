using MyAIRunningMate.Client.Geocoder.Responses;

namespace MyAIRunningMate.Client.Geocoder.Extensions;

public static class GeocodeExtensions
{
    public static string ToReadableLocation(this GeocodeReverseResponse response)
    {
        if (response?.Address == null) return "Unknown Location";

        var addr = response.Address;

        var feature = addr.Park ?? addr.Amenity ?? addr.Neighbourhood ?? addr.Residential ?? addr.Suburb;
        var cityOrTown = addr.City ?? addr.Town ?? addr.Village ?? addr.CityDistrict;
        var country = addr.Country;

        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(feature)) parts.Add(feature);
        if (!string.IsNullOrWhiteSpace(cityOrTown)) parts.Add(cityOrTown);
        if (!string.IsNullOrWhiteSpace(country)) parts.Add(country);

        return parts.Count > 0 ? string.Join(", ", parts) : "Unknown Location";
    }
}