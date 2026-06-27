using System.Text.Json;
using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class TimeSeriesRecordEntityMapper
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    public static TimeSeriesRecordEntity ToEntity(IEnumerable<TimeSeriesRecord> model, Guid activityId) =>
        new()
        {
            ActivityId = activityId,
            TimeSeriesRecordsJson = JsonSerializer.Serialize(model, JsonSerializerOptions)
        };

    public static IEnumerable<TimeSeriesRecord> ToModel(TimeSeriesRecordEntity? entity)
    {
        if (entity == null || string.IsNullOrWhiteSpace(entity.TimeSeriesRecordsJson))
            return [];

        return JsonSerializer.Deserialize<List<TimeSeriesRecord>>(entity.TimeSeriesRecordsJson, JsonSerializerOptions) 
               ?? [];
    }
}