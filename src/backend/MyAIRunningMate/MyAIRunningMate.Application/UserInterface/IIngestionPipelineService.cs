using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Application.Models.ViewObjects;

namespace MyAIRunningMate.Application.UserInterface;

public interface IIngestionPipelineService
{
    Task<IngestionView> ProcessFitFileAsync(IFormFile file, Guid userId);
}