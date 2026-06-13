using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Geocoder.Responses;

public record GeocodeReverseResponse(
    [property: JsonPropertyName("address")] 
    GeocodeAddressResponse? Address
);