namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IUserContext
{
    Guid GetUserId();
    string GenerateJwtToken(string email);
}