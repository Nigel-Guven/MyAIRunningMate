using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Strava;
using Supabase;

namespace MyAIRunningMate.Database.Repository;

public class StravaResourceRepository(Client supabase) : BaseRepository<StravaResourceEntity>(supabase), IStravaResourceRepository;
