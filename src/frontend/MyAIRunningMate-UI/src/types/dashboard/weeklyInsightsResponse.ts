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

export const DEFAULT_WEEKLY_INSIGHTS: WeeklyInsightsResponse = {
  running_time_seconds: 0,
  running_moving_time_seconds: 0,
  running_distance_metres: 0,
  swimming_time_seconds: 0,
  swimming_distance_metres: 0,
  other_types: [],
  other_types_distance_metres: 0,
  other_types_time_seconds: 0,
  total_calories_burned: 0,
  total_training_score: 0,
  training_consistency_score: 0,
  morning_activities: 0,
  afternoon_activities: 0,
  evening_activities: 0,
  night_activities: 0,
  locations: [],
  rest_days: 7, // Defaults to a full recovery week if no data
  running_moving_efficiency: 0,
  paused_seconds: 0,
  body_battery_depletion: 0,
  body_battery_efficiency: 0,
  recovery_time_generated: 0,
  heart_rate_intensity_score: 0,
  volumetric_oxygen_max_trend: 0,
  volumetric_oxygen_max_diff_percent: 0,
  running_economy_index: 0,
};