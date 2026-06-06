using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Views;

public class AnalyticsDashboardDto
{
    [JsonPropertyName("year_summary")]
    public YearlyStatisticsDto Summary { get; set; } = null!;
    
    [JsonPropertyName("year_weekly_volume")]
    public IEnumerable<WeeklyInsightsDto> WeeklyVolumes { get; set; } = [];
}