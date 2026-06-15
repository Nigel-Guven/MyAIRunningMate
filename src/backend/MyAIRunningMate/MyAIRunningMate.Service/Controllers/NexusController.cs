using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.TrainingPlans;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Nexus.Requests;
using MyAIRunningMate.Contracts.Nexus.Responses;
using MyAIRunningMate.Service.Mappers;

namespace MyAIRunningMate.Service.Controllers;

[Authorize]
[ApiController]
[Route("api/nexus")]
public class NexusController(ITrainingPlanService trainingPlanService, IUserContext userContext)
    : ControllerBase
{
    [HttpPost("generate")]
    public async Task<ActionResult<TrainingPlanViewResponse>> CreateTrainingPlan([FromBody] NexusFormRequest formRequest)
    {
        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var trainingPlanView = await trainingPlanService.GenerateTrainingPlanAsync(
            userId,
            formRequest.PrimaryGoal,
            formRequest.ExperienceYears,
            formRequest.RunningLevel,
            formRequest.ScheduleLengthWeeks,
            formRequest.PoolAccess);
        
        return Ok(trainingPlanView.ToDto());
    }

    [HttpPut("finalize")]
    public async Task<ActionResult<TrainingPlanFinalizeResponse>> FinalizeTrainingPlan(
        [FromBody] TrainingPlanViewResponse plan)
    {
        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        try
        {
            var result = await trainingPlanService.FinalizeTrainingPlanAsync(userId, plan.ToDomain());

            return Ok(new TrainingPlanFinalizeResponse(
                result.TrainingPlanId,
                result.Message,
                result.EventsSaved
            ));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
