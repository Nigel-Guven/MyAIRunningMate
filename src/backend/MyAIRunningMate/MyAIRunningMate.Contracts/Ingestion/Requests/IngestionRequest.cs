using Microsoft.AspNetCore.Http;

namespace MyAIRunningMate.Contracts.Ingestion.Requests;

public record IngestionRequest(IFormFile File);