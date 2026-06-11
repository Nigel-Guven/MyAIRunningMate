using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.TrainingPlans;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Nexus;
using MyAIRunningMate.Contracts.Nexus.Requests;
using MyAIRunningMate.Contracts.Nexus.Responses;
using MyAIRunningMate.Domain.Models.ViewObjects;

namespace MyAIRunningMate.Service.Controllers;

[Authorize]
[ApiController]
[Route("api/nexus")]
public class NexusController : ControllerBase
{
    private readonly ITrainingPlanService _trainingPlanService;
    private readonly IUserContext _userContext;

    public NexusController(ITrainingPlanService trainingPlanService, IUserContext userContext)
    {
        _trainingPlanService = trainingPlanService;
        _userContext = userContext;
    }

    [HttpPost("generate")]
    public async Task<ActionResult<TrainingPlanView>> CreateTrainingPlan([FromBody] NexusFormRequest formRequest)
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var trainingPlanView = await _trainingPlanService.GenerateTrainingPlan(
            userId,
            formRequest.PrimaryGoal,
            formRequest.ExperienceYears,
            formRequest.RunningLevel,
            formRequest.ScheduleLengthWeeks,
            formRequest.PoolAccess);

        return Ok(trainingPlanView);
    }

    [HttpPut("finalize")]
    public async Task<ActionResult<NexusFinalizeResponse>> FinalizeTrainingPlan(
        [FromBody] TrainingPlanView plan)
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        try
        {
            var result = await _trainingPlanService.FinalizeTrainingPlanAsync(userId, plan);

            return Ok(new NexusFinalizeResponse
            {
                TrainingPlanId = result.TrainingPlanId,
                Message = result.Message,
                EventsSaved = result.EventsSaved,
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
