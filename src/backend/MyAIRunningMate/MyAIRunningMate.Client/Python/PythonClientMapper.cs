using MyAIRunningMate.Client.Python.Requests;
using MyAIRunningMate.Client.Python.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Client.Python;

public static class PythonClientMapper
{
    public static PythonApiActivity ToClientRequest(this Activity domain) =>
        new(
            ExerciseType: domain.ExerciseType,
            StartTime: domain.StartTime,
            DurationSeconds: domain.DurationSeconds,
            DistanceMetres: domain.DistanceMetres,
            AverageHeartRate: domain.AverageHeartRate,
            MaxHeartRate: domain.MaxHeartRate,
            TotalElevationGain: domain.TotalElevationGain,
            AverageSecondPerKilometre: domain.AverageSecondPerKilometre,
            TrainingEffect: domain.TrainingEffect
        );

    public static (Activity Activity, IEnumerable<Lap> Laps) ToDomain(this PythonApiActivityResponse response, Guid userId)
    {
        var activityId = Guid.NewGuid();

        var activity = new Activity(
            activityId: activityId,
            userId: userId,
            garminActivityId: response.GarminId,
            startTime: response.StartTime,
            exerciseType: response.Type,
            durationSeconds: response.DurationSeconds,
            distanceMetres: response.DistanceMetres,
            averageHeartRate: response.AverageHeartRate,
            maxHeartRate: response.MaxHeartRate,
            totalElevationGain: response.TotalElevationGain,
            trainingEffect: response.TrainingEffect,
            averageSecondPerKilometre: response.AverageSecondPerKilometre,
            stravaResourceId: null
        );

        var laps = response.Laps.Select(l => new Lap(
            lapId: Guid.NewGuid(),
            activityId: activityId,
            lapNumber: l.LapNumber,
            distanceMetres: l.Distance,
            durationSeconds: l.Duration,
            averageHeartRate: l.AverageHeartRate
        ));

        return (activity, laps);
    }

    public static TrainingPlan ToDomain(this PythonApiTrainingPlanResponse response, Guid userId)
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
        ));

        return new TrainingPlan(
            trainingPlanId: planId,
            createdAt: DateTime.UtcNow,
            userId: userId,
            title: response.Title,
            description: response.Description,
            startDate: response.StartDate,
            endDate: response.EndDate
        );
    }
}