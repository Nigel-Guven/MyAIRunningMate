namespace MyAIRunningMate.Domain.Models;

public record Profile
{
    public Guid UserId { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }

    public Profile(Guid userId, DateTimeOffset createdAt, DateTimeOffset? updatedAt)
    {
        if (updatedAt.HasValue && updatedAt.Value < createdAt)
            throw new ArgumentException("Profile updated timestamp cannot be earlier than its creation timestamp.", nameof(updatedAt));

        UserId = userId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}