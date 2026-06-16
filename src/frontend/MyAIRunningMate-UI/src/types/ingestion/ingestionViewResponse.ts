export interface IngestionViewResponse {
  garmin_activity_id: string;
  start_time: string;
  exercise_type: string;
  duration_seconds: number;
  distance_metres: number;
  training_effect: number;
  number_of_laps: number;
  activity_status: string;
}