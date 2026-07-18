export interface CalendarViewResponse {
  activity_id: string;
  start_time: string;
  exercise_type: string;
  duration_seconds: number;
  distance_metres: number;
  aerobic_training_effect: number;
  anaerobic_training_effect: number;
  training_effect_status: string;
}