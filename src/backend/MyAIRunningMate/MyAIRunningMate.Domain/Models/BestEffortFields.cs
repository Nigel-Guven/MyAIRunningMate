namespace MyAIRunningMate.Domain.Models;

public static class BestEffortFields
{
    public static readonly HashSet<string> ValidDistances =
    [
        "100m",
        "200m",
        "400m",
        "800m",
        "1K",
        "3K",
        "5K",
        "10K",
        "Half Marathon",
        "Marathon",
        "Ultra Marathon"
    ];
}