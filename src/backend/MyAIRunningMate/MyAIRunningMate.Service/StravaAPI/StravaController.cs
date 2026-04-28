using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Domain.Interfaces;
using MyAIRunningMate.Domain.Interfaces.Services;

namespace MyAIRunningMate.Service.StravaAPI;

[ApiController]
[Route("api/strava")]
public class StravaController : ControllerBase
{
    private readonly IStravaService _stravaService;
    private readonly IUserContext _userContext;
    
    public StravaController(IStravaService stravaService, IUserContext userContext)
    {
        _stravaService = stravaService;
        _userContext = userContext;
    }
    
    [HttpGet("connect")]
    public IActionResult Connect()
    {
        //var userId = _userContext.GetUserId();
        
        var userId = Guid.Parse("00000000-0000-0000-0000-000000000000");
        
        Response.Cookies.Append("strava-auth-user", userId.ToString(), new CookieOptions 
        { 
            HttpOnly = true, 
            Secure = false, 
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddMinutes(5) 
        });
        
        var state = Guid.NewGuid().ToString();
        return Redirect(_stravaService.GetAuthorizationUrl(state));
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string state)
    {
        if (!Request.Cookies.TryGetValue("strava-auth-user", out var userIdString))
        {
            return Unauthorized("Session expired. Please try connecting again.");
        }
    
        var userId = Guid.Parse(userIdString);
        
        var success = await _stravaService.ExchangeCodeAndSaveTokens(code, userId);

        if (!success)
        {
            return BadRequest("Failed to exchange tokens with Strava.");
        }
    
        Response.Cookies.Delete("strava-auth-user");
        
        return Redirect("http://localhost:5173/dashboard?sync=success");
    }
}