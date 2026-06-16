using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Login.Requests;

public record LoginRequest(
    [property: JsonPropertyName("email")] string Email, 
    [property: JsonPropertyName("password")] string Password
);