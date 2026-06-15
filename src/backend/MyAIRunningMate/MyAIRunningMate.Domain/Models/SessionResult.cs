namespace MyAIRunningMate.Domain.Models;

public class SessionResult
{
    public string Token { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}