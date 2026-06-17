using System.Text.Json;
using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class TimeSeriesRecordEntityMapper
{
    public static TimeSeriesRecordEntity ToEntity(IEnumerable<TimeSeriesRecord> model, Guid activityId) =>
        new()
        {
            ActivityId =  activityId,
            TimeSeriesRecordsJson = JsonSerializer.Serialize(model)
        };
}