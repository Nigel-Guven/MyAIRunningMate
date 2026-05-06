
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
        
        var authorizationUrl = _stravaApiService.GetAuthorizationUrl(state);

        return Ok(new { url = authorizationUrl });
    }

    [HttpGet("callback")]
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
        
            var frontendUrl = _configuration["Frontend:DashboardUrl"] ?? "http://localhost:5173/home";
            return Redirect($"{frontendUrl}?sync=success");
        }
        catch (Exception ex)
        {
            // Log the exception in your console / logger window
            Console.WriteLine($"[ERROR] Exception during Strava callback: {ex.Message}");
            Console.WriteLine(ex.StackTrace);

            // Return a 500 response to inspect the error in the browser network tab, instead of an empty response.
            return StatusCode(500, new 
            { 
                message = "An error occurred while processing your authorization.", 
                details = ex.Message 
            });
        }
    }
}