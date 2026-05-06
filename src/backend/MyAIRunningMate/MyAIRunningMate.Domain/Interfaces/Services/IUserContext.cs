namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IUserContext
{
    Guid GetUserId();
    string GenerateJwtToken(Guid userId, string email);
}