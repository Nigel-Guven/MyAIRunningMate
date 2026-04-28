using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Providers.StravaAPI.Responses;

public class StravaAPIAthlete
{
    [JsonPropertyName("id")]
    public long AthleteId { get; set; }
    
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
}