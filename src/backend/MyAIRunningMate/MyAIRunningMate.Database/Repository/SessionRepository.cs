using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Infrastructure;
using Supabase;

namespace MyAIRunningMate.Database.Repository;

public class SessionRepository(Client supabase) : BaseRepository<SessionEntity>(supabase), ISessionRepository;