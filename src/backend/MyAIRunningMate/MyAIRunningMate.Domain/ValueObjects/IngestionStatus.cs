using System.ComponentModel;

namespace MyAIRunningMate.Domain.ValueObjects;

public struct IngestionStatus
{
    public const string ActivityIngested = "New Activity Ingested";
    public const string ActivityExists = "Activity Exists";
}