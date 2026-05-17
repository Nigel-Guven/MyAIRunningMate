import { API_ENDPOINTS } from '../config/endpoints';
import { http } from '../config/http';
import type { TrainingPlanRequest, TrainingPlanResponse } from './nexus.types';

export const nexusService = {
 generateTrainingPlan: async ( formData: TrainingPlanRequest ): Promise<TrainingPlanResponse> => 
    http.post<TrainingPlanRequest>( API_ENDPOINTS.nexus.generate, formData ),

    //finalizeTrainingPlan: async ( formData: TrainingPlanRequest ): Promise<TrainingPlanResponse> => 
    //  http.put<TrainingPlanRequest>( API_ENDPOINTS.nexus.finalize, formData ),
};