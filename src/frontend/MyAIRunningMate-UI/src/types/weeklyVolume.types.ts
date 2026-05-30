export interface WeeklyVolumeDto {
  total_running_duration_seconds: number;
  total_running_distance_metres: number;
  total_swimming_duration_seconds: number;
  total_swimming_distance_metres: number;
  total_running_elevation_gain: number;
  mean_average_heart_rate: number;
  mean_max_heart_rate: number;
  total_training_effect: number;
  mean_training_effect: number;
  total_achievement_count: number;
  total_personal_record_count: number;
  total_personal_exercises: number;
  total_group_exercises: number;
  locations: string[];
  rest_days: number;
}