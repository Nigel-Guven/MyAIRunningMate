import { apiClient } from './client';

export const http = {
  get: async <T>(url: string) => {
      const response = await apiClient.get<T>(url);
      return response.data;
  },

  post: async <T>(url: string, body?: unknown) => {
      const response = await apiClient.post<T>(url, body);
      return response.data;
  },

  put: async <T>(url: string, body?: unknown) => {
      const response = await apiClient.put<T>(url, body);
      return response.data;
  },

  delete: async <T>(url: string) => {
      const response = await apiClient.delete<T>(url);
      return response.data;
  },
};