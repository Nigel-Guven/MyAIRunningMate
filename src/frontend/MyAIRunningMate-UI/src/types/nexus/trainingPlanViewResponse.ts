import type { TrainingPlanEventResponse } from "./trainingPlanEventResponse";
import type { TrainingPlanResponse } from "./trainingPlanResponse";

export interface TrainingPlanViewResponse {
  training_plan: TrainingPlanResponse;
  events: TrainingPlanEventResponse[];
}
