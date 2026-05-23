export interface TrainingPlanRequest {
  primary_goal: 'General Fitness' | '5k' | '10k' | 'Half Marathon' | 'Marathon';
  experience_years: '1 or Less' | '2-3' | '4+ years';
  running_level: 'Beginner' | 'Intermediate' | 'Advanced' | 'Expert';
  schedule_length_weeks: number;
  pool_access: 'None' | '25m Pool' | '50m Pool';
}

export type { TrainingPlanView, TrainingPlanEventView } from '../../../types/views/trainingPlanView';
