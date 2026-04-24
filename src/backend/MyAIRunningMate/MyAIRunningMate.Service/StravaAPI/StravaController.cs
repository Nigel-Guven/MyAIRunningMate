using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Domain.Interfaces;

namespace MyAIRunningMate.Service.StravaAPI;

[ApiController]
[Route("api/[controller]")]
public class StravaController : ControllerBase
{
    private readonly IStravaService _stravaService;
    
    public StravaController(IStravaService stravaService)
    {
        _stravaService = stravaService;
    }

    [HttpGet("connect")]
    public IActionResult Connect()
    {
        var state = Guid.NewGuid().ToString();
        var authUrl = _stravaService.GetAuthorizationUrl(state);
        return Redirect(authUrl);
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code)
    {
        var userId = Guid.Parse("some-test-guid"); 

        var success = await _stravaService.ExchangeCodeAndSaveTokens(code, userId);
    
        if (success) {
            return Redirect("http://localhost:3000/dashboard?sync=success");
        }
    
        return BadRequest("Failed to exchange Strava tokens.");
    }
    
    [HttpGet("activities")]
    public async Task<IActionResult> GetActivities()
    {
        await _stravaService.GetAllActivities();
        return Ok("Connected");
    }
    
    [HttpGet("activities/{id}")]
    public async Task<IActionResult> GetActivityById([FromQuery] string id)
    {
        var userId = Guid.Parse(Request.Headers["X-User-Id"]);

        var activity = await _stravaService.GetActivityById(userId, id);
        return Ok("Connected");
    }
}