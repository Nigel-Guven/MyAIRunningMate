using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface ICalendarService
{
    Task<AggregateArtifactDto?> GetAggregateActivity(Guid activityId);
    Task<IEnumerable<AggregateArtifactDto>> GetMonthlyAggregates(DateTime byMonth);

}