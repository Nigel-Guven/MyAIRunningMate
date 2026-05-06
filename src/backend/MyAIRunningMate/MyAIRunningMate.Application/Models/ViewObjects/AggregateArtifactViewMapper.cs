namespace MyAIRunningMate.Application.Models.ViewObjects;

public static class AggregateArtifactViewMapper
{
    public static AggregateArtifactView ToAggregateArtifactView(
        ActivityView activityView,
        IEnumerable<LapView> lapViews,
        StravaResourceView stravaResourceView,
        StravaGeomapView stravaGeomapView) => new()
    {
        ActivityId = activityView.ActivityId,
        GarminActivityId = activityView.GarminActivityId,
        StartTime = activityView.StartTime,
        DistanceMetres = activityView.DistanceMetres ?? 0.0,
        DurationSeconds = activityView.DurationSeconds,
        TrainingEffect = activityView.TrainingEffect ?? 0.0,
        AverageSecondPerKilometre = activityView.AverageSecondPerKilometre ?? 0.0,
        AverageHeartRate = activityView.AverageHeartRate,
        MaxHeartRate = activityView.MaxHeartRate,
        Laps = lapViews,

        ResourceId = stravaResourceView?.ResourceId ?? Guid.Empty,
        StravaId = stravaResourceView?.StravaId,
        Name = stravaResourceView?.Name ?? "Unnamed Activity",

        ExerciseType = activityView.ExerciseType ?? "Unknown",

        ElapsedTime = stravaResourceView?.ElapsedTime,
        AverageCadence = stravaResourceView?.AverageCadence,
        TotalElevationGain = activityView.TotalElevationGain ?? 0.0,
        ElevationLow = stravaResourceView?.ElevationLow,
        ElevationHigh = stravaResourceView?.ElevationHigh,

        AchievementCount = stravaResourceView?.AchievementCount ?? 0,
        KudosCount = stravaResourceView?.KudosCount ?? 0,
        PersonalRecordCount = stravaResourceView?.PersonalRecordCount ?? 0,
        AthleteCount = stravaResourceView?.AthleteCount ?? 0,

        Map = stravaGeomapView
    };
}