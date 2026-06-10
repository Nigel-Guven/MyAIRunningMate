namespace MyAIRunningMate.Contracts.Weight.Responses;

public record WeightResponse
(
    Guid Id,
    double WeightInPounds,
    DateTime CreatedAt
);