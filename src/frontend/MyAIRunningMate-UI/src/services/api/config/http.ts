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

  postWithConfig: async <T>(
    url: string,
    body?: unknown,
    config = {}
  ): Promise<T> => {
    const res = await apiClient.post<T>(
      url,
      body,
      config
    );
    return res.data;
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