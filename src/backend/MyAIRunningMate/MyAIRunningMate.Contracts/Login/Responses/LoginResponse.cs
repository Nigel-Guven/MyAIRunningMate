namespace MyAIRunningMate.Contracts.Login.Responses;

public record LoginResponse(
    string Token,
    Guid UserId
);