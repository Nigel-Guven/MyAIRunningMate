using MyAIRunningMate.Application.Models;

namespace MyAIRunningMate.Application.LinkProvider;

public interface ILinkProviderService
{
    Task<StravaResource?> FindAndLinkMatchAsync(Activity activity);
}