using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface ILinkProviderService
{
    Task<Guid?> FindAndLinkMatchAsync(ActivityDto activity);
}