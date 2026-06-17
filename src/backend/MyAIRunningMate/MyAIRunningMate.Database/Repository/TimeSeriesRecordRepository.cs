using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Repository;

public class TimeSeriesRecordRepository(Supabase.Client supabase) : BaseRepository<TimeSeriesRecordEntity>(supabase), ITimeSeriesRecordRepository
{
    private readonly Supabase.Client _supabase = supabase;
    
    public async Task InsertAsync(IEnumerable<TimeSeriesRecord> timeSeriesRecords, Guid activityId)
    {
        var entity = TimeSeriesRecordEntityMapper.ToEntity(timeSeriesRecords, activityId);
        
        await _supabase.From<TimeSeriesRecordEntity>().Insert(entity);
        
    }
}