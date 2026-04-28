using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Domain.Interfaces;
using MyAIRunningMate.Domain.Interfaces.Services;

namespace MyAIRunningMate.Application.Strava;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid GetUserId()
    {
        var user = httpContextAccessor.HttpContext?.User;

        var userIdClaim = user?.FindFirst("sub")?.Value 
                          ?? user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UnauthorizedAccessException("User ID not found in the current security context.");
        }

        return Guid.Parse(userIdClaim);
    }
}