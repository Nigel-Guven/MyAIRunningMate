using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Providers.MyAIRunningMateApi;

namespace MyAIRunningMate.Service.SessionAPI;

[ApiController]
[Route("api/session")]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var token = await _sessionService.LoginAsync(request.Email, request.Password);
        
        if (token == null)
            return BadRequest(new { message = "Could not create session" });
        
        var isStravaConnected = await _sessionService.HasStravaConnectionAsync(token.UserId);

        var response = new LoginResponse
        {
            Token = token.Token,
            UserId = token.UserId,
            IsStravaConnected = isStravaConnected
        };

        return Ok(response); 
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await _sessionService.LogoutAsync();
            return Ok();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
}
