import { apiClient } from './apiClient';
import type { WeightEntry } from '../types/weight';

export const getWeightHistory = async (): Promise<WeightEntry[]> => {
    const response = await apiClient.get<WeightEntry[]>('/weight/history');
    // Reverse for chronological chart display
    return response.data.reverse();
};

export const logWeight = async (pounds: number): Promise<WeightEntry> => {
    const response = await apiClient.post<WeightEntry>('/weight/log_weight', {
        weight_pounds: pounds
    });
    return response.data;
};