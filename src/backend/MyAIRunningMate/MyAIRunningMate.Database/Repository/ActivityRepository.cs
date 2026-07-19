using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class ActivityRepository(Supabase.Client supabase) : BaseRepository<ActivityEntity>(supabase), IActivityRepository
{
    private readonly Supabase.Client _supabase = supabase;

    public async Task<IEnumerable<Activity>> GetAllActivitiesByMonth(DateTime byMonth, Guid userId)
    {
        var startOfMonth = new DateTime(byMonth.Year, byMonth.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var startOfNextMonth = startOfMonth.AddMonths(1);

        var result = await _supabase
            .From<ActivityEntity>()
            .Where(x => x.StartTime >= startOfMonth)
            .Where(x => x.StartTime < startOfNextMonth)
            .Where(x => x.UserId == userId)
            .Get();

        return result.Models.Select(entity => entity.ToDomain());
    }

    public async Task<IEnumerable<Activity>> GetAllActivitiesByYear(DateTime byYear, Guid userId)
    {
        var startOfYear = new DateTime(byYear.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var startOfNextYear = startOfYear.AddYears(1);

        var result = await _supabase
            .From<ActivityEntity>()
            .Where(x => x.StartTime >= startOfYear)
            .Where(x => x.StartTime < startOfNextYear)
            .Where(x => x.UserId == userId)
            .Get();

        return result.Models.Select(entity => entity.ToDomain()).OrderBy(x => x.StartTime);
    }

    public async Task<IEnumerable<Guid>> GetCurrentWeekActivityIds(Guid userId, DateTime firstDateOfWeek,  DateTime lastDateOfWeek)
    {
        var result = await _supabase
            .From<ActivityEntity>()
            .Select(x => new object[] { x.ActivityId }) 
            .Where(x => x.StartTime >= firstDateOfWeek)
            .Where(x => x.StartTime < lastDateOfWeek)
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

        return result.Models.Count != 0;
    }

    public async Task<Activity?> GetActivityByActivityId(Guid activityId, Guid userId)
    {
        var result = await _supabase
            .From<ActivityEntity>() 
            .Where(x => x.ActivityId == activityId) 
            .Where(x => x.UserId == userId) 
            .Limit(1) 
            .Get(); 

        return result.Model?.ToDomain();
    }

    public async Task<List<Activity>> GetLatestActivities(Guid userId)
    {
        var result = await _supabase
            .From<ActivityEntity>()
            .Where(x => x.UserId == userId)
            .Order(x => x.GarminActivityId, Constants.Ordering.Descending)
            .Limit(10)
            .Get();

        return result.Models.Select(entity => entity.ToDomain()).ToList();
    }

    public async Task<Activity> GetLatestActivity(Guid userId)
    {
        var result = await _supabase.From<ActivityEntity>()
            .Filter("user_id", Constants.Operator.Equals, userId.ToString())
            .Order("start_time", Constants.Ordering.Descending) 
            .Limit(1)                                  
            .Get();
        
        var latestEntity = result.Models.FirstOrDefault();

        return latestEntity?.ToDomain();
    }
    
    public async Task<Weight> GetLatestWeight(Guid userId)
    {
        var result = await _supabase.From<WeightEntity>()
            .Filter("user_id", Constants.Operator.Equals, userId.ToString())
            .Order("created_at", Constants.Ordering.Descending) 
            .Limit(1)                                  
            .Get();
        
        return result.Models.FirstOrDefault().ToDomain();
    }

    public async Task<Guid> InsertAsync(Activity activity)
    {
        var entity = activity.ToEntity();
        
        var activityData = new Dictionary<string, object?>
        {
            { "id", entity.ActivityId },
            { "user_id", entity.UserId },
            { "garmin_activity_id", entity.GarminActivityId },
            { "start_time", entity.StartTime },
            { "elapsed_time", entity.TotalTime },
            { "moving_time", entity.MovingTime },
            { "distance_metres", entity.DistanceMetres },
            { "beginning_body_battery", entity.BeginningBodyBattery },
            { "beginning_body_potential", entity.BeginningBodyPotential },
            { "ending_body_battery", entity.EndingBodyBattery },
            { "ending_potential", entity.EndingPotential },
            { "total_ascent", entity.TotalAscent },
            { "total_descent", entity.TotalDescent },
            { "recovery_time", entity.RecoveryTime },
            { "exercise_type", entity.ExerciseType },
            { "exercise_subtype", entity.ExerciseSubType },
            { "exercise_name", entity.ExerciseName },
            { "user_volumetric_oxygen_max", entity.UserVolumetricOxygenMax },
            { "user_max_heart_rate", entity.UserMaxHeartRate },
            { "user_lactate_threshold_heart_rate", entity.UserLactateThresholdHeartRate },
            { "user_lactate_threshold_power", entity.UserLactateThresholdPower },
            { "user_lactate_threshold_speed", entity.UserLactateThresholdSpeed },
            { "number_of_laps", entity.NumberOfLaps },
            { "location", entity.Location },
            { "map_polyline", entity.MapPolyline }
        };

        var parameters = new Dictionary<string, object>
        {
            { "activity_data", activityData }
        };

        var response = await _supabase.Rpc("insert_activity", parameters);

        if (response?.Content == null)
            throw new InvalidOperationException("Failed to insert activity via RPC or parse returned ID.");

        var trimmedId = response.Content.Trim('"');
        return Guid.TryParse(trimmedId, out var insertedId) ? insertedId : throw new InvalidOperationException("Failed to insert activity via RPC or parse returned ID.");
    }
}