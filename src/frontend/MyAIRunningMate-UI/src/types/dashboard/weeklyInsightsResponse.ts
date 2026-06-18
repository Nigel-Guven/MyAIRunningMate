export interface WeeklyInsightsResponse {
  running_time_seconds: number;
  running_moving_time_seconds: number;
  running_distance_metres: number;
  total_running_elevation_gain: number;
  swimming_time_seconds: number;
  swimming_distance_metres: number;
  total_calories_burned: number;
  mean_average_heart_rate: number;
  mean_max_heart_rate: number;
  total_training_effect: number;
  mean_training_effect: number;
  morning_activities: number;
  afternoon_activities: number;
  evening_activities: number;
  night_activities: number;
  locations: string[] | [];
  rest_days: number | 7;
  running_time_break_seconds: number;
  elevation_intensity: number;
  caloric_intensity: number;
  running_moving_efficiency: number;
}