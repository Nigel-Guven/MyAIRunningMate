using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.TrainingPlans;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Nexus;

namespace MyAIRunningMate.Service.NexusGeminiApi;

[ApiController]
[Route("api/nexus")]
public class NexusGeminiApiController : ControllerBase
{
    private readonly ITrainingPlanService _trainingPlanService;
    private readonly IUserContext _userContext;

    public NexusGeminiApiController(ITrainingPlanService trainingPlanService, IUserContext userContext)
    {
        _trainingPlanService = trainingPlanService;
        _userContext =  userContext;
    }
    
    [HttpPost("generate")]
    public async Task<ActionResult> CreateTrainingPlan([FromBody] NexusRequest request)
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        var trainingPlanView = await _trainingPlanService.GenerateTrainingPlan(
            userId, 
            request.PrimaryGoal,
            request.RunningExperienceInYears,
            request.RunningLevel,
            request.TrainingPlanLength,
            request.PoolSize );

        return Ok(trainingPlanView);
    }
    
    [HttpPut("finalize")]
    public async Task<ActionResult> FinalizeTrainingPlan()
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        return Ok();
    }
}