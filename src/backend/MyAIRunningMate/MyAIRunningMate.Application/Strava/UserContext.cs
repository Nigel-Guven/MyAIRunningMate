using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyAIRunningMate.Domain.Interfaces.Services;

namespace MyAIRunningMate.Application.Strava;

public class UserContext(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : IUserContext
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

        if (Guid.TryParse(userIdClaim, out var userId))
        {
            return userId;
        }

        throw new FormatException("User ID claim could not be parsed as a GUID.");
    }
    
    public string GenerateJwtToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        

        var key = 
            Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured."));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, GetUserId().ToString()),
                new Claim(ClaimTypes.Email, email)
            ]),

            
            
            
            
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}