import { apiClient } from './apiClient';
import type { WeightRequest } from '../types/weightRequest';
import type { WeightViewDto } from '../types/weightView';

export const getLatestWeight = async (): Promise<WeightViewDto> => {
    const response = await apiClient.get<WeightViewDto>('/weight/single');
    return response.data;
};

export const getWeightHistory = async (): Promise<WeightViewDto[]> => {
    const response = await apiClient.get<WeightViewDto[]>('/weight/latest');
    // Reverse for chronological chart display
    return response.data.reverse();
};

export const logWeight = async (pounds: number): Promise<WeightRequest> => {
    const response = await apiClient.post<WeightRequest>('/weight/log_new', {
        weight_pounds: pounds
    });
    return response.data;
};