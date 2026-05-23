export interface TrainingPlanRequest {
  primaryGoal: 'General Fitness' | '5k' | '10k' | 'Half Marathon' | 'Marathon';
  experienceYears: '1 or Less' | '2-3' | '4+ years';
  runningLevel: 'Beginner' | 'Intermediate' | 'Advanced' | 'Expert';
  scheduleLengthWeeks: number;
  poolAccess: 'None' | '25m Pool' | '50m Pool';
}

export type { TrainingPlanView, TrainingPlanEventView } from '../../../types/views/trainingPlanView';
