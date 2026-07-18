export interface LapResponse {
  lap_number: number;
  lap_start_time: string;
  duration_seconds: number;
  distance_metres: number;
  average_speed: number;
  average_heart_rate: number;
  max_heart_rate: number;
  average_cadence: number | null;
  primary_stroke: string | null;
  number_of_lengths: number | null;
}