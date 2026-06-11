using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class SessionRepository(Supabase.Client supabase) : BaseRepository<SessionEntity>(supabase), ISessionRepository
{
    public async Task<Session?> GetSessionByUserId(Guid userId)
    {
        var response = await Supabase.From<SessionEntity>()
            .Filter("user_id", Constants.Operator.Equals, userId.ToString())
            .Get();
        
        return response.Models.FirstOrDefault()?.ToDomain();
    }

    public async Task SaveSession(Session session)
    {
        SessionEntity entityToUpsert = session.ToEntity();

        await Upsert(entityToUpsert);
    }
}