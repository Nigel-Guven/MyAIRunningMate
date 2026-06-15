using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.ValueObjects;

namespace MyAIRunningMate.Application.IngestionPipeline;

public interface IIngestionPipelineService
{
    Task<(Activity activity, int numberOfLaps, string status)> ProcessFitFileAsync(IFormFile file, Guid userId);
}