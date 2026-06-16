import { apiClient } from './client';
import { authStorage } from './authStorage';

export const setupInterceptors = () => {
  apiClient.interceptors.request.use(
    (config) => {
      const token = authStorage.get();
      if (token && config.headers) {
        config.headers['Authorization'] = `Bearer ${token}`;
      }
      return config;
    },
    (error) => Promise.reject(error)
  );

  apiClient.interceptors.response.use(
    (response) => response,
    async (error) => {
      const originalRequest = error.config;

      if (error.response?.status === 401 && !originalRequest?._retry) {
        originalRequest._retry = true;
        authStorage.clear();
        window.location.href = '/login';
      }

      return Promise.reject(error);
    }
  );
};