using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Nexus;

namespace MyAIRunningMate.Service.NexusGeminiApi;

[ApiController]
[Route("api/nexus")]
public class NexusGeminiApiController : ControllerBase
{
    private readonly IUserContext _userContext;

    public NexusGeminiApiController(IUserContext userContext)
    {
        _userContext =  userContext;
    }
    
    [HttpPut("plan")]
    public async Task<ActionResult> CreateTrainingPlan([FromBody] NexusRequest request)
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        return Ok();
    }
    
    [HttpPut("finalize")]
    public async Task<ActionResult> FinalizeTrainingPlan()
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        return Ok();
    }
}