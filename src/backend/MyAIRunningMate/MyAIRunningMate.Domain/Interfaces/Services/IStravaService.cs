namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IStravaService
{
    string GetAuthorizationUrl(string state);
    Task<bool> ExchangeCodeAndSaveTokens(string code, Guid userId);
}