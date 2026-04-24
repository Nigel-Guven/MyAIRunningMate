using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Strava;
using Supabase;

namespace MyAIRunningMate.Database.Repository;

public class SessionRepository(Client supabase) : BaseRepository<SessionEntity>(supabase), ISessionRepository
{
    public async Task<SessionEntity?> GetSessionByUserId(Guid userId)
    {
        return await GetById(userId);
    }

    public async Task SaveSession(SessionEntity session)
    {
        await Upsert(session);
    }
}