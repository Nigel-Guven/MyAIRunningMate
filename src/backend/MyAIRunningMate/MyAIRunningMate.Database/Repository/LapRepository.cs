using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Infrastructure;
using Supabase;

namespace MyAIRunningMate.Database.Repository;

public class LapRepository(Client supabase) : BaseRepository<LapEntity>(supabase), ILapRepository;