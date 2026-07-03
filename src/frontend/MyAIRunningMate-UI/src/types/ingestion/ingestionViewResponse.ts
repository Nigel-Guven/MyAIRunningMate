export interface IngestionViewResponse {
  garmin_activity_id: string;
  start_time: string;
  exercise_type: string;
  distance_metres: number;
  duration_seconds: number;
  recovery_time: number;
  number_of_laps: number;
  activity_status: string;
}