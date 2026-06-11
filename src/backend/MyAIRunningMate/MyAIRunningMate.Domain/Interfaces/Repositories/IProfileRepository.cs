
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface IProfileRepository
{
    Task<Profile?> GetByIdAsync(Guid userId);
    Task CreateAsync(Profile profile);
}