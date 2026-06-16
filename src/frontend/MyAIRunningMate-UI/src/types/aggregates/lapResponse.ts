export interface LapResponse {
  lap_number: number;
  distance_metres: number;
  duration_seconds: number;
  average_heart_rate: number;
  average_speed: number;
  average_cadence: number;
  primary_stroke: string | null;
  average_swolf: number | null;
}