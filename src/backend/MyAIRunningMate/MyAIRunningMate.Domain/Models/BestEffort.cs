public record BestEffort
{
    public Guid BestEffortId { get; init; }
    public Guid ActivityId { get; init; }
    public Guid UserId { get; init; }
    public string ExerciseType { get; init; }
    public double EffortDistanceMetres { get; init; }
    public string EffortDistanceLabel => ResolveDistanceLabel((int)EffortDistanceMetres);
    public double? TimeAchievement { get; init; }
    public bool IsPersonalRecord { get; init; }

    public BestEffort(
        Guid activityId, 
        Guid bestEffortId,
        Guid userId, 
        string exerciseType,
        double effortDistanceMetres, 
        double? timeAchievement,
        bool isPersonalRecord = false)
    {
        if (effortDistanceMetres <= 0)
            throw new ArgumentException("Distance in metres must be greater than zero.", nameof(effortDistanceMetres));

        if (string.IsNullOrWhiteSpace(exerciseType))
            throw new ArgumentException("Exercise type cannot be empty.", nameof(exerciseType));

        if (timeAchievement is <= 0)
            throw new ArgumentException("Time achievement seconds must be greater than zero.", nameof(timeAchievement));

        ActivityId = activityId;
        BestEffortId = bestEffortId;
        UserId = userId;
        ExerciseType = exerciseType;
        EffortDistanceMetres = effortDistanceMetres;
        TimeAchievement = timeAchievement;
        IsPersonalRecord = isPersonalRecord;
    }

    private static string ResolveDistanceLabel(int distanceMetres) =>
        distanceMetres switch
        {
            100   => "100 Metre",
            400   => "400 Metre",
            750   => "750 Metre",
            1000  => "1km",
            1500  => "1500 Metre",
            1609  => "1 mile",
            5000  => "5k",
            10000 => "10k",
            21098 => "Half Marathon",
            _     => $"{distanceMetres}m" // Fallback for unmatched distances
        };
}