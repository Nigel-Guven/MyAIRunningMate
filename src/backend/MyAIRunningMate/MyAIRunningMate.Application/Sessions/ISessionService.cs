using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Sessions;

public interface ISessionService
{
    Task<SessionResult> LoginAsync(string email, string password);
    Task LogoutAsync();
}