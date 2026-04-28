using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IFitFileService
{
    Task<ActivityDto> ProcessAndStoreFitFileAsync(IFormFile file);
}