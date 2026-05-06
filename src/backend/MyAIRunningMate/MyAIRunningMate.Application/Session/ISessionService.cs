using MyAIRunningMate.Application.Models;

namespace MyAIRunningMate.Application.Session;

public interface ISessionService
{
    Task<SessionResult> LoginAsync(string email, string password);
    Task LogoutAsync();
    Task<bool> HasStravaConnectionAsync(Guid userId);
}