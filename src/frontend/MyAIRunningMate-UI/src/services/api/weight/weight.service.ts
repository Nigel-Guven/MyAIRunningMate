import { http } from '../config/http';
import type { WeightRequest, WeightResponse } from '../../../types/weight/weight.types';
import { API_ENDPOINTS } from '../config/endpoints';

export const weightService = {
    getLatest: (): Promise<WeightResponse> => 
        http.get<WeightResponse>( API_ENDPOINTS.weight.latest ),

    getHistory: async (): Promise<WeightResponse[]> => { 
        const data = await http.get<WeightResponse[]>( API_ENDPOINTS.weight.history );

        return [...data].reverse();
    },

    log: ( pounds: number ): Promise<WeightRequest> => 
        http.post<WeightRequest>( API_ENDPOINTS.weight.logWeight, { weight_pounds: pounds, } ),
};