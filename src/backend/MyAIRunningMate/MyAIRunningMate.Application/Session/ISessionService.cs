using MyAIRunningMate.Contracts.Login.Responses;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface ISessionService
{
    Task<LoginResponse> LoginAsync(string email, string password);
    Task LogoutAsync();
    Task<bool> HasStravaConnectionAsync(Guid userId);
}