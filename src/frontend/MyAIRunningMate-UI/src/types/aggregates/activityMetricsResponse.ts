export interface ActivityMetricsResponse {
  total_cycles: number;
  total_calories: number;
  estimated_sweat_loss: number | null;
  average_temperature: number | null;
  max_temperature: number | null;
  average_heart_rate: number;
  max_heart_rate: number;
  average_power: number | null;
  max_power: number | null;
  average_cadence: number;
  max_cadence: number | null;
  average_vertical_oscillation: number | null;
  step_length: number | null;
  average_vertical_ratio: number | null;
  average_stance_time: number | null;
  aerobic_training_effect: number;
  anaerobic_training_effect: number;
  average_swolf: number | null;
  pool_length: number | null;
}