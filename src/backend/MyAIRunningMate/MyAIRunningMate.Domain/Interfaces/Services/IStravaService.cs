namespace MyAIRunningMate.Domain.Interfaces;

public interface IStravaService
{
    string GetAuthorizationUrl();
    Task<bool> ExchangeCodeAndSaveTokens(string code);
    Task SyncActivities(Guid userId);
}