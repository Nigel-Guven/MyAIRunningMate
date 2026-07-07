using System.Collections.Frozen;

namespace MyAIRunningMate.Domain.ValueObjects;

public static class BestEffortFields
{
    public static readonly FrozenSet<string> SwimmingDistances = new HashSet<string>
    {
        "100 Metre",
        "400 Metre",
        "750 Metre",
        "1km",
        "1500 Metre"
    }.ToFrozenSet();

    public static readonly FrozenSet<string> RunningDistances = new HashSet<string>
    {
        "1km",
        "1 mile",
        "5K",
        "10K",
        "Half Marathon",
        "Marathon"
    }.ToFrozenSet();
}