using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Weight.Requests;

public record LogWeightRequest(
    double WeightInPounds
);