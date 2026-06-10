using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.Insights;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Views;
using MyAIRunningMate.Service.ViewMappers;

namespace MyAIRunningMate.Service.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardInsightsController : ControllerBase
{
    private readonly IUserContext _userContext;
    private readonly IInsightsService _insightsService;
    
    public DashboardInsightsController(IUserContext userContext, IInsightsService insightsService)
    {
        _userContext = userContext;
        _insightsService = insightsService;
    }
    
    [HttpGet("volume")]
    public async Task<ActionResult<WeeklyInsightsDto>> GetWeeklyVolume()
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        var weeklyInsights = await _insightsService.GetWeeklyInsights(userId);

        var dto = weeklyInsights.ToWeeklyInsightsDto();
        
        return Ok(dto);
    }
    
    //[HttpGet("insights")]
    //public async Task<ActionResult<IEnumerable<WeeklyInsightsDto>>> GetWeeklyInsights()
    //{
    //    var userId = _userContext.GetUserId();
    //    if (userId == Guid.Empty) return Unauthorized();
    //}
}