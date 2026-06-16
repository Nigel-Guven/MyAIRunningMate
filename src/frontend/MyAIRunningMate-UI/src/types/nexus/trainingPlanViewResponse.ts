import type { TrainingPlanEventResponse } from "./trainingPlanEventResponse";
import type { TrainingPlanResponse } from "./trainingPlanResponse";

export interface TrainingPlanViewResponse {
  trainingPlanEvents: TrainingPlanEventResponse[];
  trainingPlanInfo: TrainingPlanResponse;
}
