namespace MyAIRunningMate.Contracts.Nexus.Requests;

public record NexusFormRequest(
    string PrimaryGoal,
    string ExperienceYears,
    string RunningLevel,
    int ScheduleLengthWeeks,
    string PoolAccess
);