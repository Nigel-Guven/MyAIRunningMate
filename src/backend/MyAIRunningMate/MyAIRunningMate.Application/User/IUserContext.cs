namespace MyAIRunningMate.Application.User;

public interface IUserContext
{
    Guid GetUserId();
    string GenerateJwtToken(Guid userId, string email);
}