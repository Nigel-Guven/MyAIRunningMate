namespace MyAIRunningMate.Domain.Interfaces;

public interface IStravaService
{
    string GetAuthorizationUrl(string state);
    Task<bool> ExchangeCodeAndSaveTokens(string code);
    Task GetAllActivities();
    Task GetActivityById(string userId, string id);
}