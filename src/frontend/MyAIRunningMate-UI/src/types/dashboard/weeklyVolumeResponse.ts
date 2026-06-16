export interface WeeklyInsightResponse {
  running_time_volume: number;
  running_distance_volume: number;
  swimming_time_volume: number;
  swimming_distance_volume: number;
  total_running_elevation_gain: number;
  mean_average_heart_rate: number;
  mean_max_heart_rate: number;
  total_training_effect: number;
  mean_training_effect: number;
  locations: string[];
  rest_days: number;
}