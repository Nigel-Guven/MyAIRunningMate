using MyAIRunningMate.Domain;

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
    public IActionResult Connect() => Redirect(_stravaService.GetAuthorizationUrl());

    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code)
    {
        await _stravaService.ExchangeCodeAndSaveTokens(code);
        return Ok("Connected");
    }
}