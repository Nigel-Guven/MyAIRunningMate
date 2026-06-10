namespace MyAIRunningMate.Contracts.Login.Requests;

public record LoginRequest(
    string Email, 
    string Password
);