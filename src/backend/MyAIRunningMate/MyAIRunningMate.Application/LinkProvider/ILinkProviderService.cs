
namespace MyAIRunningMate.Application.LinkProvider;

public interface ILinkProviderService
{
    Task<Guid?> FindAndLinkMatchAsync(Models.Activity activity);
}