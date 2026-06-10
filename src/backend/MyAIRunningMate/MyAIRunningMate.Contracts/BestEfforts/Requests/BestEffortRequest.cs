namespace MyAIRunningMate.Contracts.BestEfforts.Requests;

public record BestEffortRequest
(
    string DistanceLabel, 
    int NewPersonalRecordTime,
    DateTime NewPersonalRecordDate
);