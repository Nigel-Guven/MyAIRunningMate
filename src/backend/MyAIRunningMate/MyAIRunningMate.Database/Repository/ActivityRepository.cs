using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Infrastructure;
using Supabase;

namespace MyAIRunningMate.Database.Repository;

public class ActivityRepository(Client supabase) : BaseRepository<ActivityEntity>(supabase), IActivityRepository;