using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;

namespace MyAIRunningMate.Database.Repository;

public class ActivityRepository(Supabase.Client supabase) : BaseRepository<ActivityEntity>(supabase), IActivityRepository
{
    private readonly Supabase.Client _supabase = supabase;

    public async Task<IEnumerable<ActivityEntity>> GetAllActivitiesByMonth(DateTime byMonth, Guid userId)
    {
        var startOfMonth = new DateTime(byMonth.Year, byMonth.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        
        var startOfNextMonth = startOfMonth.AddMonths(1);

        var result = await _supabase
            .From<ActivityEntity>()
            .Where(x => x.StartTime >= startOfMonth)
            .Where(x => x.StartTime < startOfNextMonth)
            .Where(x => x.UserId == userId)
            .Get();

        return result.Models;
    }

    public async Task<bool> ActivityExistsByGarminId(string garminId, Guid userId)
    {
        var result = await _supabase
            .From<ActivityEntity>()
            .Where(x => x.GarminActivityId == garminId)
            .Where(x => x.UserId == userId)
            .Limit(1)
            .Get();

        return result.Models.Any();
    }

    public async Task<ActivityEntity?> GetActivityByActivityId(Guid activityId, Guid userId)
    {
        var result = await _supabase
            .From<ActivityEntity>()
            .Where(x => x.ActivityId == activityId)
            .Where(x => x.UserId == userId)
            .Limit(1)
            .Get();

        return result.Model;
    }
}