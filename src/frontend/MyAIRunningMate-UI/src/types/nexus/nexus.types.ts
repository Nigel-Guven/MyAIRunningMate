import type { NexusFormRequest } from "./nexusFormRequest";
import type { TrainingPlanFinalizeResponse } from "./trainingPlanFinalizeResponse";
import type { TrainingPlanViewResponse } from "./trainingPlanViewResponse";

export interface NexusTypes {
  nexusForm: NexusFormRequest;

  trainingPlanFinalize: TrainingPlanFinalizeResponse;

  trainingPlanView: TrainingPlanViewResponse;
}