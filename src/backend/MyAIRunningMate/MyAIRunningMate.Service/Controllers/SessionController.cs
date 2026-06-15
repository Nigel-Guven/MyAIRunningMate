using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Sessions;
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
        try
        {
            var session = await _sessionService.LoginAsync(request.Email, request.Password);

            var response = new LoginResponse(
                Token: session.Token,
                UserId: session.UserId
            );

            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await _sessionService.LogoutAsync();
            return Ok(new { message = "Logged out successfully." });
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Unable to process logout request cleanly." });
        }
    }
}
