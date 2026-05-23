import type { TrainingPlanView } from '../../../types/views/trainingPlanView';
import { API_ENDPOINTS } from '../config/endpoints';
import { http } from '../config/http';
import type { TrainingPlanRequest } from './nexus.types';

export const nexusService = {
  generateTrainingPlan: (formData: TrainingPlanRequest): Promise<TrainingPlanView> =>
    http.post<TrainingPlanView>(API_ENDPOINTS.nexus.generate, formData, {
      timeout: 300000,
    }),
};
