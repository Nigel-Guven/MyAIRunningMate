namespace MyAIRunningMate.Domain.Models;

public record BestEffort
{
    public long BestEffortId { get; init; }
    public Guid UserId { get; init; }
    public int DistanceMetres { get; init; }
    public string DistanceLabel { get; init; }
    public int? TimeAchievement { get; init; }
    public DateTime? AchievementDate { get; init; }

    public BestEffort(
        long bestEffortId, 
        Guid userId, 
        int distanceMetres, 
        string distanceLabel, 
        int? timeAchievement, 
        DateTime? achievementDate)
    {
        if (distanceMetres <= 0)
            throw new ArgumentException("Distance in metres must be greater than zero.", nameof(distanceMetres));

        if (string.IsNullOrWhiteSpace(distanceLabel))
            throw new ArgumentException("Distance label cannot be empty.", nameof(distanceLabel));

        if (timeAchievement.HasValue && timeAchievement.Value <= 0)
            throw new ArgumentException("Time achievement seconds must be greater than zero.", nameof(timeAchievement));

        BestEffortId = bestEffortId;
        UserId = userId;
        DistanceMetres = distanceMetres;
        DistanceLabel = distanceLabel;
        TimeAchievement = timeAchievement;
        AchievementDate = achievementDate;
    }
}