using MyAIRunningMate.Database.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface IProfileRepository
{
    Task<ProfileEntity?> GetByIdAsync(Guid userId);
    Task CreateAsync(ProfileEntity profile);
}