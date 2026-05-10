using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Session;
using MyAIRunningMate.Application.Strava;
using MyAIRunningMate.Application.User;

namespace MyAIRunningMate.Service.StravaAPI;


[ApiController]
[Route(ApiRoutes.StravaRoot)]
public class StravaController : ControllerBase
{
    private readonly IStravaApiService _stravaApiService;
    private readonly ISessionService _sessionService;
    private readonly IUserContext _userContext;
    private readonly IConfiguration _configuration;
    
    public StravaController(IStravaApiService stravaApiService, 
        ISessionService sessionService, 
        IUserContext userContext, 
        IConfiguration configuration)
    {
        _stravaApiService = stravaApiService;
        _sessionService = sessionService;
        _userContext = userContext;
        _configuration = configuration;
    }
    
    [Authorize]
    [HttpGet(ApiRoutes.StravaConnect)]
    public IActionResult Connect()
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        var state = userId.ToString();
        
        var authorizationUrl = _stravaApiService.GetAuthorizationUrl(state);

        return Ok(new { url = authorizationUrl });
    }

    [HttpGet(ApiRoutes.StravaCallback)]
    public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string state)
    {
        if (string.IsNullOrEmpty(state) || !Guid.TryParse(state, out var userId))
        {
            return BadRequest("Invalid state parameter.");
        }

        try
        {
            var success = await _stravaApiService.ExchangeAndSave(code, userId);

            if (!success)
            {
                return BadRequest("Failed to exchange tokens with Strava. Please check your credentials and try again.");
            }
        
            var frontendUrl = _configuration["Frontend:DashboardUrl"] ?? "http://localhost:5173/upload";
            return Redirect($"{frontendUrl}?sync=success");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Exception during Strava callback: {ex.Message}");
            Console.WriteLine(ex.StackTrace);

            return StatusCode(500, new 
            { 
                message = "An error occurred while processing your authorization.", 
                details = ex.Message 
            });
        }
    }
    
    [Authorize]
    [HttpGet(ApiRoutes.StravaStatus)]
    public async Task<IActionResult> StravaStatus()
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        var isConnected = await _sessionService.HasStravaConnectionAsync(userId);
        
        return Ok(new { isStravaConnected = isConnected });
    }
}