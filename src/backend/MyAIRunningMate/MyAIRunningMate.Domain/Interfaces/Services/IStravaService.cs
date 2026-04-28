using MyAIRunningMate.Domain.Models.Activities;
using MyAIRunningMate.Domain.Service.Responses;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IStravaService
{
    string GetAuthorizationUrl(string state);
    Task<bool> ExchangeCodeAndSaveTokens(string code, Guid userId);
    Task<IEnumerable<StravaApiEventResponse>> GetLatestStravaActivities(Guid userId, int amount);
    
}