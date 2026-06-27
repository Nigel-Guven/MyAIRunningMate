using System.Net;
using System.Text.Json;
using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class ActivityRepository(Supabase.Client supabase, ITimeSeriesRecordRepository timeSeriesRecordRepository) : BaseRepository<ActivityEntity>(supabase), IActivityRepository
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

        return result.Models.Select(entity => entity.ToDomain());
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

    public async Task<Activity> InsertAsync(Activity activity, IEnumerable<Lap> laps)
    {
        var activityPayload = new Dictionary<string, object?> {
            { "user_id", activity.UserId },
            { "garmin_activity_id", activity.GarminActivityId },
            { "start_time", activity.StartTime },
            { "exercise_type", activity.ExerciseType },
            { "duration_seconds", activity.DurationSeconds },
            { "moving_time_seconds", activity.MovingTimeSeconds },
            { "distance_metres", activity.DistanceMetres },
            { "calories", activity.Calories },
            { "average_heart_rate", activity.AverageHeartRate },
            { "max_heart_rate", activity.MaxHeartRate },
            { "total_elevation_gain", activity.TotalElevationGain },
            { "training_effect", activity.TrainingEffect },
            { "raw_pace_seconds_per_metre", activity.RawPaceSecondsPerMetre },
            { "pool_length", activity.PoolLength },
            { "location", activity.Location },
            { "map_polyline", activity.MapPolyline }
        };
    
        var lapsPayload = laps.Select(l => new Dictionary<string, object?> {
            { "lap_number", l.LapNumber },
            { "distance_metres", l.DistanceMetres },
            { "duration_seconds", l.DurationSeconds },
            { "average_heart_rate", l.AverageHeartRate },
            { "average_speed", l.AverageSpeed },
            { "average_cadence", l.AverageCadence },
            { "primary_stroke", l.PrimaryStroke },
            { "average_swolf", l.AverageSwolf }
        });
        
        var response = await _supabase.Rpc("save_activity_with_laps", new { 
            activity_metadata = activityPayload, 
            laps_data = lapsPayload 
        });
    
        if (response.ResponseMessage.StatusCode != HttpStatusCode.OK )
        {
            throw new Exception($"RPC Error: {response.ResponseMessage} | Content: {response.Content}");
        }
    
        var newActivityId = Guid.Parse(response.Content.Trim('"'));

        if (activity.TimeSeriesRecords != null)
            await timeSeriesRecordRepository.InsertAsync(activity.TimeSeriesRecords, newActivityId);

        return activity;
    }
}