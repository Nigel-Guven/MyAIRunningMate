export interface TrainingPlanEventResponse {
  training_plan_id: string;
  event_date: string;
  exercise_type: string;
  exercise_subtype: string;
  description: string;
  distance_metres: number;
}