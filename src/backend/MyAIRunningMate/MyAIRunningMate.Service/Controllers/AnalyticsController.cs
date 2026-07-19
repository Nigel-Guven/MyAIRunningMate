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
        
        var queryDate = new DateTime(year, 1, 1);
        
        var (yearlyStats, yearlyAnalytics) = await insightsService.GetAnalyticsStatistics(userId, queryDate);

        var dashboardDto = new AnalyticsCombinedResponse(
            YearlyStatistics: yearlyStats.ToYearlyStatisticsDto(),
            YearlyAnalytics: yearlyAnalytics.ToYearlyAnalyticsDto()
        );
        
        return Ok(dashboardDto);
    }
}