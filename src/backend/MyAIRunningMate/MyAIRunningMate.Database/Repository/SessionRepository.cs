using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Strava;
using Supabase;

namespace MyAIRunningMate.Database.Repository;

public class SessionRepository(Client supabase) : BaseRepository<SessionEntity>(supabase), ISessionRepository
{
    public Task GetSessionByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }
}