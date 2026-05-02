using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Models.Requests;

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
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var success = await _sessionService.LoginAsync(request.Email, request.Password);
        
        if (!success)
            return BadRequest("Could not create session");

        return Ok(new { message = "Session created successfully" });
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
