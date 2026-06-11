using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface ISessionRepository
{
    Task<Session?> GetSessionByUserId(Guid userId);
    Task SaveSession(Session session);
}