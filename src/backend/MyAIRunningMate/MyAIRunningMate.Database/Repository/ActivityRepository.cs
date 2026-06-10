using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;
using Supabase.Postgrest;

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

    public async Task<IEnumerable<ActivityEntity>> GetAllActivitiesByYear(DateTime byYear, Guid userId)
    {
        var startOfYear = new DateTime(byYear.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        var startOfNextYear = startOfYear.AddYears(1);

        var result = await _supabase
            .From<ActivityEntity>()
            .Where(x => x.StartTime >= startOfYear)
            .Where(x => x.StartTime < startOfNextYear)
            .Where(x => x.UserId == userId)
            .Get();

        return result.Models;
    }

    public async Task<IEnumerable<Guid>> GetCurrentWeekActivityIds(Guid userId)
    {
        var now = DateTime.UtcNow;

        var daysSinceMonday = (int)now.DayOfWeek - (int)DayOfWeek.Monday;
    
        if (daysSinceMonday < 0)
        {
            daysSinceMonday += 7;
        }

        var startOfWeek = now.Date.AddDays(-daysSinceMonday);
        var startOfWeekUtc = DateTime.SpecifyKind(startOfWeek, DateTimeKind.Utc);
        var startOfNextWeekUtc = startOfWeekUtc.AddDays(7);

        var result = await _supabase
            .From<ActivityEntity>()
            .Select(x => new object[] { x.ActivityId}) 
            .Where(x => x.StartTime >= startOfWeekUtc)
            .Where(x => x.StartTime < startOfNextWeekUtc)
            .Where(x => x.UserId == userId)
            .Get();

        return result.Models.Select(x => x.ActivityId);
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
        var result = await _supabase .From<ActivityEntity>() 
            .Where(x => x.ActivityId == activityId) 
            .Where(x => x.UserId == userId) 
            .Limit(1) 
            .Get(); 
        
        return result.Model;
    }

    public async Task<List<ActivityEntity>> GetLatestActivities(Guid userId)
    {
        var result = await _supabase
            .From<ActivityEntity>()
            .Where(x => x.UserId == userId)
            .Order(x => x.GarminActivityId, Constants.Ordering.Descending)
            .Limit(10)
            .Get();

        return result.Models;
    }
}