namespace MyAIRunningMate.Domain.Models;

public record StravaResource
{
    public Guid ResourceId { get; init; }
    public string StravaId { get; init; }
    public string Name { get; init; }
    public long ElapsedTime { get; init; }
    public double DistanceMetres { get; init; }
    public double TotalElevationGain { get; init; }
    public double? AverageCadence { get; init; }
    public string Type { get; init; }
    public DateTime StartDate { get; init; }
    public long AchievementCount { get; init; }
    public long KudosCount { get; init; }
    public long AthleteCount { get; init; }
    public long PersonalRecordCount { get; init; }
    public double ElevationLow { get; set; }
    public double ElevationHigh { get; set; }
    public Guid? MapId { get; init; }

    public StravaResource(
        Guid resourceId,
        string stravaId,
        string name,
        long elapsedTime,
        double distanceMetres,
        double totalElevationGain,
        double? averageCadence,
        string type,
        DateTime startDate,
        long achievementCount,
        long kudosCount,
        long athleteCount,
        long personalRecordCount,
        double elevationLow,
        double elevationHigh,
        Guid? mapId)
    {
        if (string.IsNullOrWhiteSpace(stravaId))
            throw new ArgumentException("Strava activity ID identifier cannot be null or empty.", nameof(stravaId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Resource activity name cannot be null or empty.", nameof(name));

        if (elapsedTime < 0)
            throw new ArgumentException("Elapsed tracking time cannot be negative.", nameof(elapsedTime));

        if (distanceMetres < 0)
            throw new ArgumentException("Activity distance track cannot be negative.", nameof(distanceMetres));

        if (achievementCount < 0 || kudosCount < 0 || athleteCount < 0 || personalRecordCount < 0)
            throw new ArgumentException("Activity social counters and record trackers cannot be negative metrics.");

        ResourceId = resourceId;
        StravaId = stravaId;
        Name = name;
        ElapsedTime = elapsedTime;
        DistanceMetres = distanceMetres;
        TotalElevationGain = totalElevationGain;
        AverageCadence = averageCadence;
        Type = type;
        StartDate = startDate;
        AchievementCount = achievementCount;
        KudosCount = kudosCount;
        AthleteCount = athleteCount;
        PersonalRecordCount = personalRecordCount;
        ElevationLow = elevationLow;
        ElevationHigh = elevationHigh;
        MapId = mapId;
    }
}