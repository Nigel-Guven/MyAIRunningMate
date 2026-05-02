namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface ISessionService
{
    Task<bool> LoginAsync(string email, string password);
    Task LogoutAsync();
}