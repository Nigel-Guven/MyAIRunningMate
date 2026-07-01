using MyAIRunningMate.Client.Helpers;
using MyAIRunningMate.Client.Python.Responses.Ingestion;
using MyAIRunningMate.Client.Python.Responses.TrainingPlan;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Client.Python;

public static class PythonClientMapper
{
    public static AggregateArtifact ToAggregateArtifact(
        this PythonApiActivityResponse response, 
        Guid userId,
        string? mapPolyline,
        string? location)
    {
        var activityId = Guid.NewGuid();

        var activity = ClientToDomainMapperFactory.ToDomainActivity(
            response.ActivityMetricsResponse, 
            response.ActivityUserMetricsResponse,
            response.ActivitySport, 
            activityId, 
            userId, 
            response.ActivityGarminId, 
            location, 
            mapPolyline);
        
        var activityMetrics = response.ActivitySessions.Select(sesh =>
            ClientToDomainMapperFactory.ToDomainActivityMetrics(sesh, activityId));

        var timeSeriesRecords = response.ActivityTimeSeries?.Select(ClientToDomainMapperFactory.ToDomainTimeSeriesRecord);
        
        var laps = response.ActivityLaps.Select(lap => ClientToDomainMapperFactory.ToDomainLap(lap, activityId));
        
        var bestEfforts = response.ActivityBestEfforts?.Select(be => 
            ClientToDomainMapperFactory.ToDomainBestEffort(be, activityId, userId, activity.ExerciseType ));

        return new AggregateArtifact(
            GarminActivity: activity,
            GarminActivityMetrics: activityMetrics,
            Laps: laps,
            TimeSeriesRecords: timeSeriesRecords,
            BestEfforts: bestEfforts);
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