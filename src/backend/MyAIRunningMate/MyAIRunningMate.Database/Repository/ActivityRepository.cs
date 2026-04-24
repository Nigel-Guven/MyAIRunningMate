using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Garmin;
using Supabase;

namespace MyAIRunningMate.Database.Repository;

public class ActivityRepository(Client supabase) : BaseRepository<ActivityEntity>(supabase), IActivityRepository
{
    private readonly Client _supabase = supabase;

    public async Task<IEnumerable<ActivityEntity>> GetAllActivities()
    {
        return await GetAll();
    }

    public async Task<IEnumerable<ActivityEntity>> GetAllActivitiesByMonth(DateTime month)
    {
        var startOfMonth = new DateTime(month.Year, month.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        
        var startOfNextMonth = startOfMonth.AddMonths(1);

        var result = await _supabase
            .From<ActivityEntity>()
            .Where(x => x.StartTime >= startOfMonth)
            .Where(x => x.StartTime < startOfNextMonth)
            .Get();

        return result.Models;
    }
    
    public async Task<IEnumerable<ActivityEntity>> GetAllActivitiesByDay(DateTime day)
    {
        var startOfDay = DateTime.SpecifyKind(day.Date, DateTimeKind.Utc);
        
        var startOfNextDay  = startOfDay.AddDays(1);

        var result = await _supabase
            .From<ActivityEntity>()
            .Where(x => x.StartTime >= startOfDay)
            .Where(x => x.StartTime < startOfNextDay)
            .Get();

        return result.Models;
    }
}