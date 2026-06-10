using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Session;
using MyAIRunningMate.Contracts.Login.Requests;
using MyAIRunningMate.Contracts.Login.Responses;

namespace MyAIRunningMate.Service.Controllers;

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
        var session = await _sessionService.LoginAsync(request.Email, request.Password);
        
        if (session == null)
            return BadRequest(new { message = "Could not create session" });
        
        var isStravaConnected = await _sessionService.HasStravaConnectionAsync(session.UserId);

        var response = new LoginResponse
        {
            Token = session.Token,
            UserId = session.UserId,
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
