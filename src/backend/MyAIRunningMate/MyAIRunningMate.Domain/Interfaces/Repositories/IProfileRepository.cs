using MyAIRunningMate.Domain.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface IProfileRepository
{
    Task<ProfileEntity?> GetByIdAsync(Guid userId);
    Task CreateAsync(ProfileEntity profile);
}