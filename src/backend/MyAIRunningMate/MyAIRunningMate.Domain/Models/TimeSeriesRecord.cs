namespace MyAIRunningMate.Domain.Models;

public record TimeSeriesRecord
{
    public DateTime Timestamp { get; init; }
    public double? DistanceMetres { get; init; }
    public int? HeartRate { get; init; }
    public int? Cadence { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }

    public TimeSeriesRecord(
        DateTime timestamp,
        double? distanceMetres,
        int? heartRate,
        int? cadence,
        double? latitude,
        double? longitude)
    {
        if (distanceMetres is < 0)
            throw new ArgumentException("Distance cannot be a negative value.", nameof(distanceMetres));

        if (heartRate is < 0)
            throw new ArgumentException("Heart rate cannot be a negative value.", nameof(heartRate));

        if (cadence is < 0)
            throw new ArgumentException("Cadence cannot be a negative value.", nameof(cadence));

        if (latitude is < -90 or > 90)
            throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90 degrees.");

        if (longitude is < -180 or > 180)
            throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180 degrees.");

        Timestamp = timestamp;
        DistanceMetres = distanceMetres;
        HeartRate = heartRate;
        Cadence = cadence;
        Latitude = latitude;
        Longitude = longitude;
    }
}