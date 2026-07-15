using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Insights;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Analytics.Responses;
using MyAIRunningMate.Service.Mappers;

namespace MyAIRunningMate.Service.Controllers;

[Authorize]
[ApiController]
[Route("api/dashboard")]
public class DashboardInsightsController(IUserContext userContext, IInsightsService insightsService) : ControllerBase
{
    [HttpGet("insights")]
    public async Task<ActionResult<WeeklyInsightsResponse>> GetWeeklyInsights([FromQuery] int weekOffset = 0)
    {
        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        var weeklyInsights = await insightsService.GetWeeklyInsights(userId, weekOffset);

        var dto = weeklyInsights.ToWeeklyInsightsDto();
        
        return Ok(dto);
    }
    
    [HttpGet("fitness-profile")]
    public async Task<ActionResult<WeeklyInsightsResponse>> GetUserFitnessMetrics()
    {
        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        var userMetrics = await insightsService.GetUserMetrics(userId);

        var dto = userMetrics.ToUserMetricsDto();
        
        return Ok(dto);
    }
}