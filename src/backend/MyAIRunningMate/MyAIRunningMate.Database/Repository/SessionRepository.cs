using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Session;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class SessionRepository(Supabase.Client supabase) : BaseRepository<SessionEntity>(supabase), ISessionRepository
{
    public async Task<SessionEntity?> GetSessionByUserId(Guid userId)
    {
        var response = await Supabase.From<SessionEntity>()
            .Filter("user_id", Constants.Operator.Equals, userId.ToString())
            .Get();
        
        return response.Models.FirstOrDefault();
    }

    public async Task SaveSession(SessionEntity session)
    {
        await Upsert(session);
    }
}