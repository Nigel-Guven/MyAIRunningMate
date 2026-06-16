using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Insights;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Analytics.Responses;
using MyAIRunningMate.Service.Mappers;

namespace MyAIRunningMate.Service.Controllers;

[Authorize]
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(IUserContext userContext, IInsightsService insightsService) : ControllerBase
{
    [HttpGet("statistics")]
    public async Task<ActionResult<YearlyStatisticsResponse>> GetYearlyStatistics([FromQuery] int year)
    {
        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        var (summary, weeklyVolumes) = await insightsService.GetAnalyticsStatistics(userId, year);

        var dashboardDto = new YearlyAnalyticsResponse(
            Summary: summary.ToYearlyStatisticsDto(),
            WeeklyVolumes: weeklyVolumes.Select(v => v.ToWeeklyInsightsDto()).ToList()
        );
        
        return Ok(dashboardDto);
    }
}