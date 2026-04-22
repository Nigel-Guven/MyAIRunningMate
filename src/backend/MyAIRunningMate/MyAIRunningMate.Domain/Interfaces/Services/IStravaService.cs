namespace MyAIRunningMate.Domain.Interfaces;

public interface IStravaService
{
    string GetAuthorizationUrl();
    Task<bool> ExchangeCodeAndSaveTokens(string code);
    Task GetAllActivities();
    Task GetActivityById(string userId, string id);
}