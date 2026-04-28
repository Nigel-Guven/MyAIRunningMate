using MyAIRunningMate.Domain.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Repositories.Session;

public interface ISessionRepository : IBaseRepository<SessionEntity> 
{
    Task<SessionEntity?> GetSessionByUserId(Guid userId);
    Task SaveSession(SessionEntity session);
    
}