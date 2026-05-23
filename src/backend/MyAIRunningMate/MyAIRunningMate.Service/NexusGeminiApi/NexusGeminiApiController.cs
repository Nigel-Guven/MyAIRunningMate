using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Application.TrainingPlans;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Nexus;

namespace MyAIRunningMate.Service.NexusGeminiApi;

[Authorize]
[ApiController]
[Route("api/nexus")]
public class NexusGeminiApiController : ControllerBase
{
    private readonly ITrainingPlanService _trainingPlanService;
    private readonly IUserContext _userContext;

    public NexusGeminiApiController(ITrainingPlanService trainingPlanService, IUserContext userContext)
    {
        _trainingPlanService = trainingPlanService;
        _userContext = userContext;
    }

    [HttpPost("generate")]
    public async Task<ActionResult<TrainingPlanView>> CreateTrainingPlan([FromBody] NexusRequest request)
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var trainingPlanView = await _trainingPlanService.GenerateTrainingPlan(
            userId,
            request.PrimaryGoal,
            request.ExperienceYears,
            request.RunningLevel,
            request.ScheduleLengthWeeks,
            request.PoolAccess);

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
