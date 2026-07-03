import type { TrainingPlanFinalizeResponse } from '../../../types/nexus/trainingPlanFinalizeResponse';
import type { TrainingPlanResponse } from '../../../types/nexus/trainingPlanResponse';
import type { TrainingPlanViewResponse } from '../../../types/nexus/trainingPlanViewResponse';
import { API_ENDPOINTS } from '../config/endpoints';
import { http } from '../config/http';

export const nexusService = {
  generateTrainingPlan: (formData: TrainingPlanResponse): Promise<TrainingPlanViewResponse> =>
    http.post<TrainingPlanViewResponse>(API_ENDPOINTS.nexus.generate, formData, {
      timeout: 300000,
    }),

  finalizeTrainingPlan: (plan: TrainingPlanViewResponse): Promise<TrainingPlanFinalizeResponse> =>
    http.put<TrainingPlanFinalizeResponse>(API_ENDPOINTS.nexus.finalize, plan),
};
