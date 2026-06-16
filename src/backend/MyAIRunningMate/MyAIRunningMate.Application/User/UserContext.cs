using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MyAIRunningMate.Application.User;

public class UserContext(IHttpContextAccessor httpContextAccessor)
    : IUserContext
{

    public Guid GetUserId()
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user?.Identity?.IsAuthenticated != true)
        {
            throw new UnauthorizedAccessException("The request is not authenticated.");
        }

        var userIdClaim = user.FindFirst("sub")?.Value ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userIdClaim, out var userId) ? userId : throw new FormatException("Invalid User ID.");
    }

    public string? GetRawToken()
    {
        var authHeader = httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }
        return authHeader["Bearer ".Length..].Trim();
    }
}