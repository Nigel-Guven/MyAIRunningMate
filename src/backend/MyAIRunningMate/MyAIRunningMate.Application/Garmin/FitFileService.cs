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
    private readonly ILapRepository _lapRepository;

    public FitFileService(IPythonApiClient pythonApiClient, IActivityRepository activityRepository, ILapRepository lapRepository)
    {
        _pythonApiClient = pythonApiClient;
        _activityRepository = activityRepository;
        _lapRepository = lapRepository;
    }
    
    public async Task<ActivityDto> ProcessAndStoreFitFileAsync(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var activityDto = await _pythonApiClient.UploadFitFileAsync(stream, file.FileName);

        if (activityDto == null) throw new Exception("Parser returned no data.");
        
        var activityId = Guid.NewGuid();
        activityDto.ActivityId = activityId;
        
        var activityEntity = MapToActivityEntity(activityDto);
        await _activityRepository.Insert(activityEntity);

        if (!activityDto.Laps.Any()) return activityDto;
        var lapEntities = activityDto.Laps.Select(l => new LapEntity
        {
            LapId = Guid.NewGuid(),
            ActivityId = activityId, 
            LapNumber = l.LapNumber,
            DistanceMetres = l.Distance,
            DurationSeconds = l.Duration,
            AverageHeartRate = l.AverageHeartRate
        }).ToList();

        await _lapRepository.BulkInsert(lapEntities);

        return activityDto;
    }

    private static ActivityEntity MapToActivityEntity(ActivityDto dto)
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
        };
    }
}