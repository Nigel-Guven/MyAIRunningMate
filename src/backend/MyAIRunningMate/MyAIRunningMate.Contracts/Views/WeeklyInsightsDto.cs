using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Views;

public class WeeklyInsightsDto
{
    [JsonPropertyName("total_running_duration_seconds")]
    public double RunningTimeVolume { get; set; }
    
    [JsonPropertyName("total_running_distance_metres")]
    public double RunningDistanceVolume { get; set; }
    
    [JsonPropertyName("total_swimming_duration_seconds")]
    public double SwimmingTimeVolume { get; set; }
    
    [JsonPropertyName("total_swimming_distance_metres")]
    public double SwimmingDistanceVolume { get; set; }
    
    [JsonPropertyName("total_running_elevation_gain")]
    public double TotalRunningElevationGain { get; set; }
    
    [JsonPropertyName("mean_average_heart_rate")]
    public int MeanAverageHeartRate { get; set; }
    
    [JsonPropertyName("mean_max_heart_rate")]
    public int MeanMaxHeartRate { get; set; }
    
    [JsonPropertyName("total_training_effect")]
    public double TotalTrainingEffect { get; set; }
    
    [JsonPropertyName("mean_training_effect")]
    public double MeanTrainingEffect { get; set; }
    
    [JsonPropertyName("total_achievement_count")]
    public long TotalAchievementCount { get; set; }
    
    [JsonPropertyName("total_personal_record_count")]
    public long TotalPersonalRecordCount { get; set; }
    
    [JsonPropertyName("total_personal_exercises")]
    public long TotalPersonalExercises { get; set; }
    
    [JsonPropertyName("total_group_exercises")]
    public long TotalGroupExercises { get; set; }
    
    [JsonPropertyName("locations")]
    public IEnumerable<string> Locations { get; set; }
    
    [JsonPropertyName("rest_days")]
    public int RestDays { get; set; }
}