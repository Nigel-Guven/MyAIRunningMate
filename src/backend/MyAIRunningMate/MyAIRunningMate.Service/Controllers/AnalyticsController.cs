using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Insights;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Analytics.Responses;
using MyAIRunningMate.Service.ViewMappers;

namespace MyAIRunningMate.Service.Controllers;

[Authorize]
[ApiController]
[Route("api/analytics")]
public class AnalyticsController  : ControllerBase
{
    private readonly IUserContext _userContext;
    private readonly IInsightsService _insightsService;
    
    public AnalyticsController(IUserContext userContext, IInsightsService insightsService)
    {
        _userContext = userContext;
        _insightsService = insightsService;
    }
    
    [HttpGet("statistics")]
    public async Task<ActionResult<YearlyStatisticsResponse>> GetYearlyStatistics([FromQuery] int year)
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        var (summary, weeklyVolumes) = await _insightsService.GetAnalyticsStatistics(userId, year);

        var dashboardDto = new YearlyAnalyticsResponse()
        {
            Summary = summary.ToYearlyStatisticsDto(),
            WeeklyVolumes = weeklyVolumes.Select(w => w.ToWeeklyInsightsDto()).ToList()
        };
        
        return Ok(dashboardDto);
    }
}