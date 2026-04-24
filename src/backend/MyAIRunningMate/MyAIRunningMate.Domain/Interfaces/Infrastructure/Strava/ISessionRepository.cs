using MyAIRunningMate.Domain.Entities;

namespace MyAIRunningMate.Domain.Interfaces.Infrastructure.Strava;

public interface ISessionRepository : IBaseRepository<SessionEntity> 
{
    Task GetSessionByUserId(Guid userId);
}