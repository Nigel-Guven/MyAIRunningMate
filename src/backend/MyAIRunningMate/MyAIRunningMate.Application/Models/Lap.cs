using System.Text.Json.Serialization;

namespace MyAIRunningMate.Domain.Models;

public class Lap
{
    public int LapNumber { get; set; }
    public double Distance { get; set; }
    public double Duration { get; set; }
    public int AverageHeartRate { get; set; }
}