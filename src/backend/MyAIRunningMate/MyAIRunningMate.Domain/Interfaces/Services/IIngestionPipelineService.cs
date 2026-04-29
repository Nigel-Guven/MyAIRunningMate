using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IIngestionPipelineService
{
    Task<IngestionViewDto> ProcessFitFileAsync(IFormFile file, Guid userId);
}