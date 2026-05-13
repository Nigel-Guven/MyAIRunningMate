using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Views;

public class WeeklyVolumeView
{
    [JsonPropertyName("average_max_heart_Rate")]
    public int AverageMaxHeartRate { get; set; }
    
    [JsonPropertyName("training_effect_this_week")]
    public int AverageTrainingEffectThisWeek { get; set; }
    
    [JsonPropertyName("running_time_this_week")]
    public int RunningTimeThisWeek { get; set; }
    
    [JsonPropertyName("running_distance_last_week")]
    public int RunningDistanceLastWeek { get; set; }

    [JsonPropertyName("running_distance_this_week")]
    public int RunningDistanceThisWeek { get; set; }
    
    [JsonPropertyName("planned_running_distance_last_week")]
    public int PlannedRunningDistanceLastWeek { get; set; }
    
    [JsonPropertyName("planned_running_distance_this_week")]
    public int PlannedRunningDistanceThisWeek { get; set; }

    [JsonPropertyName("total_elevation_this_week")]
    public int TotalElevationGainThisWeek { get; set; }
    
    [JsonPropertyName("swimming_time_this_week")]
    public int SwimmingDistanceThisWeek { get; set; }
}