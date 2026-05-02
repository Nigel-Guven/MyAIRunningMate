using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Domain.Interfaces.Services;

namespace MyAIRunningMate.Service.StravaAPI;

[ApiController]
[Route("api/strava")]
public class StravaController : ControllerBase
{
    private readonly IStravaApiService _stravaApiService;
    private readonly IUserContext _userContext;
    private readonly IConfiguration _configuration;
    
    public StravaController(IStravaApiService stravaApiService, IUserContext userContext, IConfiguration configuration)
    {
        _stravaApiService = stravaApiService;
        _userContext = userContext;
        _configuration = configuration;
    }
    
    [HttpGet("connect")]
    public IActionResult Connect()
    {
        var userId = _userContext.GetUserId();
        
        var state = userId.ToString();
        
        return Redirect(_stravaApiService.GetAuthorizationUrl(state));
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string state)
    {
        if (string.IsNullOrEmpty(state) || !Guid.TryParse(state, out var userId))
        {
            return BadRequest("Invalid state parameter.");
        }
        
        var success = await _stravaApiService.ExchangeAndSave(code, userId);

        if (!success)
        {
            return BadRequest("Failed to exchange tokens with Strava.");
        }
        
        var frontendUrl = _configuration["Frontend:DashboardUrl"] ?? "http://localhost:5173/dashboard";
        return Redirect($"{frontendUrl}?sync=success");
    }
    
    /*[HttpGet("activities")]
    public async Task<IActionResult> GetActivities([FromQuery] int amount = 10)
    {
        try
        {
            var userId = _userContext.GetUserId();
            var activities = await _stravaApiService.GetLatestStravaActivities(userId, amount);
            
            return Ok(activities);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to load activities", error = ex.Message });
        }
    }*/
}