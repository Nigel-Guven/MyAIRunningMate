export interface TrainingPlanEventView {
  eventDate: string;
  exerciseType: string;
  exerciseSubtype: string;
  description: string;
  distanceMetres: number;
}

export interface TrainingPlanView {
  title: string;
  startDate: string;
  endDate: string;
  description: string;
  trainingPlanEvents: TrainingPlanEventView[];
}
