using System.IO.Compression;
using System.Text;
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
    
    public static TimeSeriesRecordEntity ToEntity(IEnumerable<TimeSeriesRecord> model, Guid activityId)
    {
        var json = JsonSerializer.Serialize(model, JsonSerializerOptions);
        var compressed = Compress(json);

        return new TimeSeriesRecordEntity
        {
            ActivityId = activityId,
            TimeSeriesRecordsJson = compressed
        };
    }

    public static IEnumerable<TimeSeriesRecord> ToModel(TimeSeriesRecordEntity? entity)
    {
        if (entity?.TimeSeriesRecordsJson == null || entity.TimeSeriesRecordsJson.Length == 0)
            return [];

        var json = Decompress(entity.TimeSeriesRecordsJson);

        return JsonSerializer.Deserialize<List<TimeSeriesRecord>>(json, JsonSerializerOptions)
               ?? [];
    }
    
    private static byte[] Compress(string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);

        using var output = new MemoryStream();
        using (var gzip = new GZipStream(output, CompressionLevel.Optimal))
        {
            gzip.Write(bytes, 0, bytes.Length);
        }

        return output.ToArray();
    }

    private static string Decompress(byte[] bytes)
    {
        using var input = new MemoryStream(bytes);
        using var gzip = new GZipStream(input, CompressionMode.Decompress);
        using var output = new MemoryStream();

        gzip.CopyTo(output);

        return Encoding.UTF8.GetString(output.ToArray());
    }
}