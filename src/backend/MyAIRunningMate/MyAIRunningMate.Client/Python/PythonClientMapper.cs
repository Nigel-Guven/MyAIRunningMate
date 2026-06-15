using MyAIRunningMate.Client.Geocoder.Extensions;
using MyAIRunningMate.Client.Python.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Client.Python;

public static class PythonClientMapper
{
    public static (Activity Activity, IEnumerable<Lap> Laps) ToDomain(
        this PythonApiActivityResponse response, 
        Guid userId,
        string? mapPolyline,
        string? location)
    {
        var activityId = Guid.NewGuid();
        
        var timeSeriesRecords = response.TimeSeries.Select(ts => new TimeSeriesRecord(
            timestamp: ts.TimeStamp,
            distanceMetres: ts.DistanceMetres,
            heartRate: ts.HeartRate,
            cadence: ts.Cadence,
            latitude: ts.Latitude,
            longitude: ts.Longitude
        )).ToList();
        
        var activity = new Activity(
            activityId: activityId,
            userId: userId,
            garminActivityId: response.GarminId,
            startTime: response.StartTime,
            exerciseType: response.Type,
            durationSeconds: response.DurationSeconds,
            movingTimeSeconds: response.MovingTimeSeconds,
            distanceMetres: response.DistanceMetres,
            calories: response.Calories,
            averageHeartRate: response.AverageHeartRate,
            maxHeartRate: response.MaxHeartRate,
            rawPaceSecondsPerMetre: response.RawPaceSecondsPerMetre,
            totalElevationGain: response.TotalElevationGain,
            trainingEffect: response.TrainingEffect,
            poolLength: response.PoolLength,
            location: location,
            mapPolyline: mapPolyline,
            timeSeriesRecords: timeSeriesRecords
        );

        var laps = response.Laps.Select(l => new Lap(
            lapId: Guid.NewGuid(),
            activityId: activityId,
            lapNumber: l.LapNumber,
            distanceMetres: l.DistanceMetres,
            durationSeconds: l.DurationSeconds,
            averageHeartRate: l.AverageHeartRate,
            averageSpeed: l.AverageSpeed,
            averageCadence: l.AverageCadence,
            primaryStroke: l.PrimaryStroke,
            averageSwolf: l.AverageSwolf
        ));

        return (activity, laps);
    }

    public static (TrainingPlan TrainingPlan, IEnumerable<TrainingPlanEvent> Events) ToDomain(this PythonApiTrainingPlanResponse response, Guid userId)
    {
        var planId = Guid.NewGuid();
        
        var events = response.TrainingPlanEvents.Select(e => new TrainingPlanEvent(
            trainingPlanEventId: Guid.NewGuid(),
            createdAt: DateTime.UtcNow,
            trainingPlanId: planId,
            eventDate: e.EventDate,
            exerciseType: e.ExerciseType,
            exerciseSubtype: e.ExerciseSubtype,
            description: e.Description,
            distanceMetres: e.DistanceMetres
        )).ToList();
        
        var trainingPlan = new TrainingPlan(
            trainingPlanId: planId,
            createdAt: DateTime.UtcNow,
            title: response.Title,
            startDate: response.StartDate,
            endDate: response.EndDate,
            userId: userId,
            description: response.Description
        );

        return (trainingPlan, events);
    }
}