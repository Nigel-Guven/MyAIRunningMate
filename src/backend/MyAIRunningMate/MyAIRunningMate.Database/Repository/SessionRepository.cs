using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Session;
using Supabase.Postgrest;
using Client = Supabase.Client;

namespace MyAIRunningMate.Database.Repository;

public class SessionRepository(Client supabase) : BaseRepository<SessionEntity>(supabase), ISessionRepository
{
    public async Task<SessionEntity?> GetSessionByUserId(Guid userId)
    {
        var response = await _supabase.From<SessionEntity>()
            .Filter("user_id", Constants.Operator.Equals, userId.ToString())
            .Get();
        
        return response.Models.FirstOrDefault();
    }

    public async Task SaveSession(SessionEntity session)
    {
        await Upsert(session);
    }
}