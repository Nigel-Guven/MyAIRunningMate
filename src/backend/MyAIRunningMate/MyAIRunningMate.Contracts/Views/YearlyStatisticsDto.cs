using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Views;

public class YearlyStatisticsDto
{
    [JsonPropertyName("year_running_distance")]
    public int YearlyRunningDistance { get; set; }
    
    [JsonPropertyName("year_swimming_distance")]
    public int YearlySwimmingDistance { get; set; }
    
    [JsonPropertyName("year_active_days")]
    public int YearlyActiveDays { get; set; }
    
    [JsonPropertyName("year_average_training_effect")]
    public double? YearlyAverageTrainingEffect { get; set; }
    
    [JsonPropertyName("year_total_training_effect")]
    public double? YearlyTotalTrainingEffect { get; set; }
}