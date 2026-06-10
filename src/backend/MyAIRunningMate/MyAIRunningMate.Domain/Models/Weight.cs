namespace MyAIRunningMate.Domain.Models;

public class Weight
{
    public Guid Id { get; }
    public double WeightInPounds { get; }
    public Guid UserId { get; }
    public DateTime CreatedAt { get; }
    
    public Weight(Guid id, double weightInPounds, Guid userId, DateTime? createdAt = null)
    {
        if (weightInPounds <= 0) 
            throw new ArgumentException("Weight must be greater than zero.");

        Id = id;
        WeightInPounds = weightInPounds;
        UserId = userId;
        CreatedAt = createdAt ?? DateTime.UtcNow;
    }
}