using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Insights;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Analytics.Responses;
using MyAIRunningMate.Service.Mappers;

namespace MyAIRunningMate.Service.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardInsightsController(IUserContext userContext, IInsightsService insightsService) : ControllerBase
{
    [HttpGet("volume")]
    public async Task<ActionResult<WeeklyInsightsResponse>> GetWeeklyVolume()
    {
        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        var weeklyInsights = await insightsService.GetWeeklyInsights(userId);

        var dto = weeklyInsights.ToWeeklyInsightsDto();
        
        return Ok(dto);
    }
}