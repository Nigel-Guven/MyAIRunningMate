using Dynastream.Fit;
using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Models.Activities;
using DateTime = System.DateTime;

namespace MyAIRunningMate.Application.Garmin;

public class FitFileParserService : IFitFileParserService
{
    public async Task<ActivityDto> ProcessFile(IFormFile file)
    {
        return await Task.Run(() =>
        {
            using Stream stream = file.OpenReadStream();
            
            var broadcaster = new MesgBroadcaster();
            var messageAccumulator = new FitMessages();
            broadcaster.MesgEvent += messageAccumulator.SessionMesgs;

            var decode = new Decode();
            decode.Read(stream, broadcaster);

            // 2. Use the Parser logic to organize sessions/laps
            var parser = new ActivityParser(messageAccumulator);
            var parsedSessions = parser.ParseSessions();

            // 3. Map the first session (usually there is only one per FIT file)
            var sessionData = parsedSessions.FirstOrDefault();
            if (sessionData == null) 
                throw new Exception("No valid activity session found in FIT file.");

            return MapToActivityDto(sessionData);
        });
    }
    
    private ActivityDto MapToActivityDto(SessionMessages sessionData)
    {
        var session = sessionData.Session;
        var startTime = session.GetStartTime()?.GetDateTime() ?? DateTime.UtcNow;
        var distance = (double)(session.GetTotalDistance() ?? 0);
        var duration = (double)(session.GetTotalTimerTime() ?? 0);

        return new ActivityDto
        {
            ActivityId = Guid.NewGuid(),
            // Extract Serial Number from FileId for a better Unique ID
            GarminActivityId = $"G-{sessionData.FileId?.GetSerialNumber()}-{startTime.Ticks}",
            StartTime = startTime,
            ExerciseType = session.GetSport()?.ToString() ?? "Unknown",
            DurationSeconds = duration,
            DistanceMetres = distance,
            AverageHeartRate = (int)(session.GetAvgHeartRate() ?? 0),
            MaxHeartRate = (int)(session.GetMaxHeartRate() ?? 0),
            TotalElevationGain = (double)(session.GetTotalAscent() ?? 0),
            TrainingEffect = (double)(session.GetTotalTrainingEffect() ?? 0),
            AverageSecondPerKilometre = distance > 0 ? (duration / (distance / 1000)) : 0,
            
            // Map Laps
            Laps = sessionData.Laps.Select((lap, index) => new LapDto
            {
                LapId = Guid.NewGuid(),
                LapNumber = index + 1,
                Distance = (double)(lap.GetTotalDistance() ?? 0),
                Duration = (double)(lap.GetTotalTimerTime() ?? 0),
                AverageHeartRate = (int)(lap.GetAvgHeartRate() ?? 0)
            }).ToList()
        };
    }
}