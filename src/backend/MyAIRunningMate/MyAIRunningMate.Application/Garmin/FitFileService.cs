using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Client;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Garmin;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Models.Activities;

namespace MyAIRunningMate.Application.Garmin;

public class FitFileService : IFitFileService
{
    private readonly IPythonApiClient _pythonApiClient;
    private readonly IActivityRepository _activityRepository;

    public FitFileService(IPythonApiClient pythonApiClient, IActivityRepository activityRepository)
    {
        _pythonApiClient = pythonApiClient;
        _activityRepository = activityRepository;
    }
    
    public async Task<ActivityDto> ProcessAndStoreFitFileAsync(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var activityDto = await _pythonApiClient.UploadFitFileAsync(stream, file.FileName);

        if (activityDto == null) throw new Exception("Parser returned no data.");
        
        activityDto.ActivityId = Guid.NewGuid();
        foreach (var lap in activityDto.Laps)
        {
            lap.LapId = Guid.NewGuid();
        }

        var entity = MapToEntity(activityDto);

        await _activityRepository.Insert(entity); 

        return activityDto;
    }

    private static ActivityEntity MapToEntity(ActivityDto dto)
    {
        return new ActivityEntity
        {
            ActivityId = Guid.NewGuid(),
            GarminActivityId = dto.GarminActivityId,
            StartTime = dto.StartTime,
            ExerciseType = dto.ExerciseType,
            DurationSeconds = dto.DurationSeconds,
            DistanceMetres = dto.DistanceMetres,
            AverageHeartRate = dto.AverageHeartRate,
            MaxHeartRate = dto.MaxHeartRate,
            TotalElevationGain = dto.TotalElevationGain ?? 0.0,
            AverageSecondPerKilometre = dto.AverageSecondPerKilometre,
            TrainingEffect = dto.TrainingEffect,
            StravaResourceId = Guid.Empty,
            CreatedAt =  DateTime.UtcNow,
        };
    }
}