using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Geocoder.Responses;

public record GeocodeAddressResponse(
    [property: JsonPropertyName("amenity")] 
    string? Amenity,
    
    [property: JsonPropertyName("park")] 
    string? Park,
    
    [property: JsonPropertyName("suburb")] 
    string? Suburb,
    
    [property: JsonPropertyName("city_district")] 
    string? CityDistrict,
    
    [property: JsonPropertyName("neighbourhood")] 
    string? Neighbourhood,
    
    [property: JsonPropertyName("residential")] 
    string? Residential,
    
    [property: JsonPropertyName("state_district")] 
    string? StateDistrict,
    
    [property: JsonPropertyName("city")] 
    string? City,
    
    [property: JsonPropertyName("town")] 
    string? Town,
    
    [property: JsonPropertyName("village")] 
    string? Village,
    
    [property: JsonPropertyName("country")] 
    string? Country
);