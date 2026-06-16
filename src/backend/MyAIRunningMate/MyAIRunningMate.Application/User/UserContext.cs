using Microsoft.AspNetCore.Http;

namespace MyAIRunningMate.Application.User;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid GetUserId()
    {
        var user = httpContextAccessor.HttpContext?.User;
        
        var userIdClaim = user?.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UnauthorizedAccessException("User ID (sub) claim not found.");
        }

        return Guid.TryParse(userIdClaim, out var userId) 
            ? userId 
            : throw new FormatException("Invalid User ID format.");
    }
}