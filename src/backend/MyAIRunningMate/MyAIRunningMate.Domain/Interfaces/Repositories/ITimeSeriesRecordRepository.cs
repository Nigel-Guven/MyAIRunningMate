using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface ITimeSeriesRecordRepository
{ 
    Task InsertAsync(IEnumerable<TimeSeriesRecord> timeSeriesRecords, Guid activityId);
    Task<IEnumerable<TimeSeriesRecord>> GetTimeSeriesRecordsByActivityId(Guid activityId);
}