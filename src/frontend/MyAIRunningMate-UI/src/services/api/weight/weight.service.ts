import { http } from '../config/http';
import { API_ENDPOINTS } from '../config/endpoints';
import type { WeightResponse } from '../../../types/weight/weightResponse';
import type { WeightRequest } from '../../../types/weight/weightRequest';

export const weightService = {
    getLatest: (): Promise<WeightResponse> => 
        http.get<WeightResponse>( API_ENDPOINTS.weight.latest ),

    getHistory: async (): Promise<WeightResponse[]> => { 
        const data = await http.get<WeightResponse[]>( API_ENDPOINTS.weight.history );

        return [...data].reverse();
    },

    log: ( pounds: number ): Promise<WeightRequest> => 
        http.post<WeightRequest>( API_ENDPOINTS.weight.logWeight, { weight_in_pounds: pounds, } ),
};