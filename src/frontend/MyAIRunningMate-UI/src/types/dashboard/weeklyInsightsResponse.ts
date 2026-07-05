export interface WeeklyInsightsResponse {
  running_time_seconds: number;
  running_moving_time_seconds: number;
  running_distance_metres: number;
  swimming_time_seconds: number;
  swimming_distance_metres: number;
  other_types: string[] | [];
  other_types_distance_metres: number;
  other_types_time_seconds: number;
  total_calories_burned: number;
  total_training_score: number;
  training_consistency_score: number;
  morning_activities: number;
  afternoon_activities: number;
  evening_activities: number;
  night_activities: number;
  locations: string[] | [];
  rest_days: number | 7;
  running_moving_efficiency: number;
  paused_seconds: number;
  body_battery_depletion: number;
  body_battery_efficiency: number;
  recovery_time_generated: number;
  heart_rate_intensity_score: number;
  volumetric_oxygen_max_trend: number;
  volumetric_oxygen_max_diff_percent: number;
  running_economy_index: number;
}