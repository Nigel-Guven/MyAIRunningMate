import { apiClient } from './client';
import { API_ENDPOINTS } from './endpoints';

export const authApi = {
    login: async (email: string, password: string) => {
        const response = await apiClient.post(
            API_ENDPOINTS.session.login,
            {
                email,
                password,
            }
        );

        return response.data;
    },
};