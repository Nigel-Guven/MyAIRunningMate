using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Client;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Garmin;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Strava;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Models.Activities;

namespace MyAIRunningMate.Application.Garmin;

public class FitFileService : IFitFileService
{
    private readonly IPythonApiClient _pythonApiClient;
    private readonly IActivityRepository _activityRepository;
    private readonly IStravaResourceRepository _stravaRepository;
    private readonly IStravaResourceMapRepository _mapRepository;
    private readonly IStravaService _stravaService;
    private readonly ILapRepository _lapRepository;

    public FitFileService(
        IPythonApiClient pythonApiClient, 
        IActivityRepository activityRepository, 
        ILapRepository lapRepository,
        IStravaResourceRepository stravaRepository,
        IStravaResourceMapRepository mapRepository,
        IStravaService stravaService)
    {
        _pythonApiClient = pythonApiClient;
        _activityRepository = activityRepository;
        _lapRepository = lapRepository;
        _stravaRepository = stravaRepository;
        _mapRepository = mapRepository;
        _stravaService = stravaService;
    }
    
    public async Task<ActivityDto> ProcessAndStoreFitFileAsync(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var activityDto = await _pythonApiClient.UploadFitFileAsync(stream, file.FileName);
        
        var existingActivity = await _activityRepository.ActivityExistsByGarminId(activityDto.GarminActivityId); 
        
        if (existingActivity != null)
        {
            return new ActivityDto
            {
                GarminActivityId = activityDto.GarminActivityId,
                ExerciseType = activityDto.ExerciseType,
                StartTime = activityDto.StartTime,
                DistanceMetres = activityDto.DistanceMetres,
                TrainingEffect = activityDto.TrainingEffect
            };
        }
        
        Guid? stravaResourceId = Guid.NewGuid();
        
        try
        {
            var stravaActivities = await _stravaService.GetLatestStravaActivities(Guid.Empty, 5);
            
            var match = stravaActivities.FirstOrDefault(s => 
                Math.Abs((s.StartDate - activityDto.StartTime).TotalMinutes) < 2 &&
                Math.Abs(s.DistanceMetres - activityDto.DistanceMetres) < 50);

            if (match != null)
            {
                stravaResourceId = Guid.NewGuid();
                Guid? mapId = null;
                
                if (!string.IsNullOrEmpty(match.Map?.SummaryPolyline))
                {
                    mapId = Guid.NewGuid();
                    var mapEntity = new StravaResourceMapEntity
                    {
                        MapId = mapId.Value,
                        MapPolyline = match.Map.SummaryPolyline,
                    };
                    
                    await _mapRepository.Insert(mapEntity);
                }
                
                var stravaEntity = new StravaResourceEntity()
                {
                    ResourceId = stravaResourceId.Value,
                    StravaId = match.StravaId.ToString(),
                    Name = match.Name,
                    ElapsedTime = match.ElapsedTime,
                    DistanceMetres = match.DistanceMetres,
                    TotalElevationGain =  match.TotalElevationGain,
                    AverageCadence =  match.AverageCadence,
                    Type = match.Type,
                    StartDate = match.StartDate,
                    AchievementCount =  match.AchievementCount,
                    KudosCount = match.KudosCount,
                    AthleteCount = match.AthleteCount,
                    PersonalRecordCount =   match.PersonalRecordCount,
                    ElevationLow =  match.ElevationLow,
                    ElevationHigh =  match.ElevationHigh,
                    MapId =  mapId
                };
            
                await _stravaRepository.Insert(stravaEntity);
            } 
            
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Strava Match Sync Failed: {ex.Message}");
        }
        
        if (activityDto == null) throw new Exception("Parser returned no data.");
        
        var activityId = Guid.NewGuid();
        activityDto.ActivityId = activityId;
        
        var activityEntity = MapToActivityEntity(activityDto, stravaResourceId.Value);
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

    private static ActivityEntity MapToActivityEntity(ActivityDto dto, Guid stravaResourceId)
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
            TotalElevationGain = dto.TotalElevationGain,
            AverageSecondPerKilometre = dto.AverageSecondPerKilometre,
            TrainingEffect = dto.TrainingEffect,
            StravaResourceId = stravaResourceId,
        };
    }
}