using MyAIRunningMate.Client.Python.Responses.Ingestion;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Client.Helpers;

public static class ClientToDomainMapperFactory
{
    
    
    public static Activity ToDomainActivity(
        PythonActivityMetricsResponse activityMetrics, 
        PythonUserMetricsResponse metrics, 
        PythonSportResponse sport,
        Guid activityId, Guid userId, string garminId,
        string location, string polyline) =>
        new(
            activityId: activityId,
            userId: userId,
            garminActivityId: garminId,
            startTime: activityMetrics.ActivityStartTime,
            beginningBodyBattery: metrics.UserBeginningBodyBattery,
            beginningBodyPotential: metrics.UserBeginningBodyPotential,
            endingBodyBattery: activityMetrics.ActivityEndingBodyBattery,
            endingPotential: activityMetrics.ActivityEndingPotential,
            recoveryTime: activityMetrics.ActivityRecoveryTime,
            exerciseType: sport.SportType,
            exerciseSubType: sport.SportSubType,
            exerciseName: sport.SportName,
            userVolumetricOxygenMax: metrics.UserVolumetricOxygenMax,
            userMaxHeartRate: metrics.UserMaxHeartRate,
            userLactateThresholdHeartRate: metrics.UserLactateThresholdHeartRate,
            userLactateThresholdPower: metrics.UserLactateThresholdPower,
            userLactateThresholdSpeed: metrics.UserLactateThresholdSpeed,
            numberOfLaps: activityMetrics.ActivityNumLaps,
            totalAscent: activityMetrics.ActivityTotalAscent,
            totalDescent: activityMetrics.ActivityTotalDescent,
            location: location,
            mapPolyline: polyline);

    public static TimeSeriesRecord ToDomainTimeSeriesRecord(PythonTimeSeriesResponse timeSeriesResponse) =>
        new(
            timestamp: timeSeriesResponse.TsrTimeStamp,
            distanceMetres: timeSeriesResponse.TsrDistanceMetres,
            heartRate: timeSeriesResponse.TsrHeartRate,
            cadence: timeSeriesResponse.TsrCadence,
            latitude: timeSeriesResponse.TsrLatitude,
            longitude: timeSeriesResponse.TsrLongitude
        );

    public static ActivityMetrics ToDomainActivityMetrics(PythonSessionResponse response, Guid activityId) =>
        new(
            activityId: activityId,
            elapsedSeconds: response.SessionElapsedTime,
            movingTime: response.SessionMovingTime,
            distanceMetres: response.SessionDistanceMetres,
            totalCycles: response.SessionTotalCycles,
            totalCalories: response.SessionTotalCalories,
            averageHeartRate: response.SessionAverageHeartRate,
            maxHeartRate: response.SessionMaxHeartRate,
            aerobicTrainingEffect: response.SessionAerobicTrainingEffect,
            anaerobicTrainingEffect: response.SessionAerobicTrainingEffect,
            estimatedSweatLoss: response.SessionEstimatedSweatLoss,
            averageTemperature: response.SessionAverageTemperature,
            maxTemperature: response.SessionMaxTemperature,
            averagePower: response.SessionAveragePower,
            maxPower: response.SessionMaxPower,
            averageCadence: response.SessionAverageCadence,
            maxCadence: response.SessionMaxCadence,
            averageVerticalOscillation: response.SessionAverageVerticalOscillation,
            stepLength: response.SessionStepLength,
            averageVerticalRatio: response.SessionAverageVerticalRatio,
            averageStanceTime: response.SessionAverageStanceTime,
            averageSwolf: response.SessionAverageSwolf,
            poolLength: response.SessionPoolLength
        );

    public static Lap ToDomainLap(PythonLapResponse response, Guid activityId) =>
        new(
            lapId: Guid.NewGuid(),
            activityId: activityId,
            lapNumber: response.LapNumber,
            lapStartTime: response.LapStartTime,
            distanceMetres: response.LapDistanceMetres,
            durationSeconds: response.LapDurationSeconds,
            averageHeartRate: response.LapAverageHeartRate,
            maxHeartRate: response.LapMaxHeartRate,
            averageSpeed: response.LapAverageSpeed,
            averageCadence: response.LapAverageCadence,
            primaryStroke: response.LapPrimaryStroke,
            numberOfLengths: response.LapNumberOfLengths
        );

    public static BestEffort ToDomainBestEffort(PythonBestEffortResponse response, Guid activityId, Guid userId, string exerciseType) =>
        new(
            activityId: activityId,
            bestEffortId: Guid.NewGuid(),
            userId: userId,
            exerciseType: exerciseType,
            effortDistanceMetres: response.EffortDistanceMetres,
            timeAchievement: response.EffortAchievementTime,
            isPersonalRecord: response.EffortIsPersonalRecord
        );
}